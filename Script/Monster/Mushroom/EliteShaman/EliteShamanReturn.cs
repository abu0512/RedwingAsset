using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanReturn : EliteShamanStateBase
{
    public float _attackpattern;

    public override void BeginState()
    {
        base.BeginState();
        _attackpattern = Random.Range(0, 10f);
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        EliteShaman.TurnToDestination();
        EliteShaman.GroggyCheck();
        EliteShaman.PlayerisDead();
        EliteShaman.TimeToHeal();

        if (EliteShaman.GetDistanceFromPlayer() < EliteShaman.MStat.AttackDistance)
        {
            if (EliteShaman.AttackTimer > EliteShaman.AttackDelay && _attackpattern > 4f)
            {
                EliteShaman.SetState(EliteShamanState.Attack);
                return;
            }

            else if (EliteShaman.AttackTimer > EliteShaman.AttackDelay && _attackpattern < 4f)
            {
                EliteShaman.SetState(EliteShamanState.Attack2);
                return;
            }
        }

        if (EliteShaman.GetDistanceFromPlayer() < EliteShaman.MStat.ChaseDistance && EliteShaman.GetDistanceFromPlayer() > EliteShaman.MStat.AttackDistance)
        {
            EliteShaman.SetState(EliteShamanState.Chase);
            return;
        }
    }
}
