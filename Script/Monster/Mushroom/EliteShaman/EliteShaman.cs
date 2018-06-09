using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EliteShamanState
{
    Idle = 0,
    Chase,
    Attack,
    Attack2,
    Return,
    Healing,
    Groggy
}

[RequireComponent(typeof(MonsterStat))]
public class EliteShaman : MonsterBase
{
    public Material _Mat; // 기본 매터리얼
    public Material _HitMat; // 피격 매터리얼
    public GameObject _EliteShamanMat;

    public EliteShamanState startState;
    public EliteShamanState currentState;

    private Dictionary<EliteShamanState, EliteShamanStateBase> _states = new Dictionary<EliteShamanState, EliteShamanStateBase>();

    // 플레이어 위치
    private Transform _player;
    public Transform Player { get { return _player; } }

    // 몬스터 지정된 Home 값
    private Vector3 _home;
    public Vector3 Home { get { return _home; } }

    // 투사체 방향에 사용할 변수
    private Transform _from;
    public Transform From { get { return _from; } }

    // 공격력
    float _attackDamage;
    public float AttackDamage { set { _attackDamage = value; } get { return _attackDamage; } }

    // 공격 딜레이 속도
    float _attackDelay;
    public float AttackDelay { set { _attackDelay = value; } get { return _attackDelay; } }

    // 공격 딜레이 시간
    float _attackTimer;
    public float AttackTimer { set { _attackTimer = value; } get { return _attackTimer; } }

    // 공격 횟수
    int _attackStack;
    public int AttackStack { set { _attackStack = value; } get { return _attackStack; } }

    // 내적 계산에 사용할 앵글
    private float _angle;
    public float Angle { set { _angle = value; } get { return _angle; } }

    // 그로기 수치
    private float _groggy;
    public float Groggy { set { _groggy = value; } get { return _groggy; } }

    // 그로기 수치 최대 값
    private float _maxgroggy;
    public float MaxGroggy { set { _maxgroggy = value; } get { return _maxgroggy; } }

    // 공격 상태 인 상황에서의 회전 속도
    [SerializeField]
    private float _attackrotangle;
    public float AttackRotAngle { set { _attackrotangle = value; } get { return _attackrotangle; } }

    // 플레이어 캐릭터가 전방에 있는지 후방에 있는지(처리 = 내적)
    private bool _playerisfront;
    public bool PlayerisFront { set { _playerisfront = value; } get { return _playerisfront; } }

    // 힐 딜레이 시간
    float _hearTimer;
    public float HealTimer { set { _hearTimer = value; } get { return _hearTimer; } }

    // 공격 애니메이션 싱크로 체크
    private bool _anisynchro;
    public bool AniSynchro { set { _anisynchro = value; } get { return _anisynchro; } }

    // 캐릭터 사망
    private bool _CharacterisDead;
    public bool CharacterisDead { set { _CharacterisDead = value; } get { return _CharacterisDead; } }

    public float rotAnglePerSecond = 360.0f;// 몬스터 초당 회전 속도
    public float HealDelay; // 힐 쿨타임
    public float HealPoint; // 힐량
    public bool isDead; // 죽었는지 체크
    public bool HealTime; // 힐 하는 상황인지
    public bool HealStart; // 힐 시작

    public MonsterStat MStat { get { return _stat; } set { _stat = value; } }

    private int _animParamID;
    private bool _isInit = false;

    public EliteShamanStateBase GetCurrentState()
    {
        return _states[currentState];
    }

    public void SetState(EliteShamanState newState)
    {
        if (_isInit)
        {
            _states[currentState].enabled = false;
            _states[currentState].EndState();
        }
        currentState = newState;
        _states[currentState].BeginState();
        _states[currentState].enabled = true;
        _anim.SetInteger(_animParamID, (int)currentState);
    }

    public float GetDistanceFromPlayer() // Player 캐릭터와 거리를 되돌려줄 함수
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        return distance;
    }

    public void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Player.position - transform.position);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation,
            Time.deltaTime * _attackrotangle);
    }

       public void MoveToDestination()
    {
        _controller.Move(transform.forward * _stat.MoveSpeed * Time.deltaTime);
    }

    // 움직이는 함수 (속도 입력이 포함 되어있음)
    public void GoToDestination(Vector3 target, float moveSpeed, float turnSpeed)
    {
        Transform t = _controller.transform;
        Vector3 Forward = target - t.position;
        Forward.y = 0.0f;
        if (Forward != Vector3.zero)
        {
            t.rotation = Quaternion.RotateTowards(
                t.rotation,
                Quaternion.LookRotation(Forward),
                turnSpeed * Time.deltaTime);
        }

        Vector3 nextPos = Vector3.MoveTowards(
            t.position,
            target,
            moveSpeed * Time.deltaTime);

        Vector3 deltaMove = nextPos - t.position;
        deltaMove.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(deltaMove);
    }

    public override void OnDamage(float damage)
    {
        if (_CharacterisDead)
            return;

        base.OnDamage(damage);
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
        StartCoroutine(ChangeMat());
    }

    public override void OnDead()
    {
        if (isDead == true)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddGroggyValue(float value)
    {
        if (currentState == EliteShamanState.Groggy)
            return;

        _groggy += value;
        _groggy = Mathf.Clamp(_groggy, 0, _maxgroggy);
    }

    public void GroggyCheck()
    {
        if (_groggy < 99f)
            return;

        if (_groggy >= 99f)
        {
            _groggy = 0;
            SetState(EliteShamanState.Groggy);
            return;
        }
    }

    public void GroggySet()
    {
        _groggy -= (0.5f * Time.deltaTime);
        _groggy = Mathf.Clamp(_groggy, 0, _maxgroggy);
    }

    private IEnumerator ChangeMat()
    {
        _EliteShamanMat.GetComponent<Renderer>().material = _HitMat;
        yield return new WaitForSeconds(0.2f);
        _EliteShamanMat.GetComponent<Renderer>().material = _Mat;
    }

    public void PlayerisDead()
    {
        if (CPlayerManager._instance.isDead)
        {
            SetState(EliteShamanState.Idle);
            return;
        }
    }

    public void FrontBackCheck()
    {
        float Dot = Vector3.Dot(transform.forward, (_player.position - transform.position).normalized);

        if (Dot >= Mathf.Cos(Mathf.Deg2Rad * _angle * 0.5f))
        {
            _playerisfront = true;
        }

        else
        {
            _playerisfront = false;
        }
    }

    public void TimeToHeal()
    {
        if (HealTime && _hearTimer > HealDelay)
        {
            SetState(EliteShamanState.Healing);
            return;
        }
    }

    public void SetMonsterHeal(float Heal)
    {
        Stat.Hp += Heal;
        Stat.Hp = Mathf.Clamp(Stat.Hp, 0, Stat.MaxHp);
    }

    protected override void Awake()
    {
        base.Awake();
        _stat.ChaseDistance = 20f;
        _stat.AttackDistance = 15f;
        _stat.MoveSpeed = 3.5f;
        _stat.MaxHp = 800f;
        _stat.Hp = _stat.MaxHp;
        _attackDamage = 20f;
        _attackDelay = 5f;
        _attackTimer = 0;
        _attackStack = 0;
        _hearTimer = 0;
        HealDelay = 10f;
        _angle = 180f;
        _from = transform.Find("From");
        _groggy = 0;
        _maxgroggy = 100f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _home = transform.position;
        _animParamID = Animator.StringToHash("CurrentState");
        isDead = false;
        HealTime = false;
        HealStart = false;

        EliteShamanState[] stateValues = (EliteShamanState[])Enum.GetValues(typeof(EliteShamanState));
        foreach (EliteShamanState s in stateValues)
        {
            Type FSMType = Type.GetType("EliteShaman" + s.ToString("G"));
            EliteShamanStateBase state = (EliteShamanStateBase)GetComponent(FSMType);
            if (state == null)
                state = (EliteShamanStateBase)gameObject.AddComponent(FSMType);

            state.enabled = false;
            _states.Add(s, state);
        }

        SetState(startState);
        _isInit = true;

    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            SetState(startState);
            _isInit = true;
        }
    }

    protected override void Update()
    {
        FrontBackCheck();
        GroggySet();
        AttackTimer += Time.deltaTime;
        _hearTimer += Time.deltaTime;

        if (Stat.Hp <= 0)
        {
            isDead = true;
            _anim.SetBool("isDead", true);
        }
    }
}
