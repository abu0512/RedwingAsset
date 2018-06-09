using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanAttack2 : EliteShamanStateBase
{
    private float RandomNum;
    private float _rotate;
    private int _bulletcount;
    private bool _stun;
    private bool _notstun;
    private bool _exitattack;
    public GameObject _from;

    public override void BeginState()
    {
        _bulletcount = 0;
        _rotate = 0;
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
        if (EliteShaman.AniSynchro && _stun && _bulletcount < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                Stage1.I.BulletPool.SetEliteSStunBullet(_EliteShaman, _from.transform.position, _rotate);
                _rotate += 22.5f;
                _bulletcount++;
            }
            StopCor();
        }

        else if (EliteShaman.AniSynchro && _notstun && _bulletcount < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                Stage1.I.BulletPool.SetEliteSBullet(_EliteShaman, _from.transform.position, _rotate);
                _rotate += 22.5f;
                _bulletcount++;
            }
            StopCor();
        }
    }

    public void StartESAttack()
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
