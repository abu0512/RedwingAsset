using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanAttack : EliteShamanStateBase
{
    private float RandomNum;
    private float _imsitime;
    private int _bulletcount;
    private bool _stun;
    private bool _notstun;
    private bool _exitattack;

    public override void BeginState()
    {
        Dltime = 0;
        _bulletcount = 0;
        _imsitime = 0;
        RandomNum = Random.Range(0, 9f);
        EliteShaman.AttackStack++;
    }

    public override void EndState()
    {
        base.EndState();
        _bulletcount = 0;
        _stun = false;
        _notstun = false;
        _exitattack = false;
        EliteShaman.AniSynchro = false;
        EliteShaman.AttackTimer = 0f;
    }

    public void StopCor()
    {
        if (_bulletcount > 4)
        {
            _exitattack = true;
        }
    }

    public void SetAttack()
    {
        StopCor();
        _imsitime += Time.deltaTime;
        if (EliteShaman.AniSynchro && _stun && _bulletcount < 5)
        {
            if (_imsitime > 0.15f)
            {
                Stage1.I.BulletPool.SetEliteStunBullet(_EliteShaman, transform.position, EliteShaman.Player.position);
                _bulletcount++;
                _imsitime = 0;
            }
        }

        else if (EliteShaman.AniSynchro && _notstun && _bulletcount < 5)
        {
            if (_imsitime > 0.15f)
            {
                Stage1.I.BulletPool.SetEliteBullet(_EliteShaman, transform.position, EliteShaman.Player.position);
                _bulletcount++;
                _imsitime = 0;
            }
        }
    }

    public void StartEAttack()
    {
        if (EliteShaman.AttackStack > 2 && RandomNum > 2f)
        {
            _stun = true;
            EliteShaman.AttackStack = 0;
            EliteShaman.AniSynchro = true;
        }

        else
        {
            _notstun = true;
            EliteShaman.AniSynchro = true;
        }
    }

    void Update()
    {
        SetAttack();
        EliteShaman.GroggyCheck();
        EliteShaman.PlayerisDead();
        EliteShaman.TurnToDestination();

        Dltime += Time.deltaTime;

        if (_exitattack && Dltime > 1f)
        {
            if (EliteShaman.GetDistanceFromPlayer() > EliteShaman.MStat.AttackDistance)
            {
                EliteShaman.SetState(EliteShamanState.Chase);
                return;
            }

            else
            {
                EliteShaman.SetState(EliteShamanState.Return);
                return;
            }
        }
    }
}
