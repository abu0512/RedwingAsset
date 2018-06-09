using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanIdle : EliteShamanStateBase
{

    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        EliteShaman.GroggyCheck();

        if (EliteShaman.GetDistanceFromPlayer() < EliteShaman.Stat.ChaseDistance && (CPlayerManager._instance.isDead == false))
        {
            EliteShaman.GoToDestination(EliteShaman.Player.position, EliteShaman.Stat.MoveSpeed, EliteShaman.rotAnglePerSecond);
            EliteShaman.SetState(EliteShamanState.Chase);
            return;
        }
    }
}
