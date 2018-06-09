using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanChase : EliteShamanStateBase
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
        EliteShaman.PlayerisDead();
        EliteShaman.TimeToHeal();
        EliteShaman.GroggyCheck();

        EliteShaman.GoToDestination(EliteShaman.Player.position, EliteShaman.MStat.MoveSpeed, EliteShaman.rotAnglePerSecond);

        if (EliteShaman.GetDistanceFromPlayer() < EliteShaman.MStat.AttackDistance && EliteShaman.AttackTimer > EliteShaman.AttackDelay)
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

        else if (EliteShaman.GetDistanceFromPlayer() < EliteShaman.MStat.AttackDistance && EliteShaman.AttackTimer < EliteShaman.AttackDelay)
        {
            EliteShaman.SetState(EliteShamanState.Return);
            return;
        }
    }
}
