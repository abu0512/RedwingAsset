using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanDead : EliteShamanStateBase
{
    private float DeadTime;

    public override void BeginState()
    {
        DeadTime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        if (EliteShaman.isDead)
        {
            EliteShaman.rotAnglePerSecond = 0;
            EliteShaman.AttackRotAngle = 0;
            EliteShaman.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            EliteShaman.CharacterisDead = true;

            if (DeadTime >= 1.6f)
            {
                EliteShaman.OnDead();
                return;
            }
        }
    }
}
