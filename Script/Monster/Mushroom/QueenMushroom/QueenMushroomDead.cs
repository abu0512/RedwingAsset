using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomDead : QueenMushroomStateBase
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
        if (QueenMushroom.isDead)
        {
            //SoundManager.I.PlaySound(transform.position, PlaySoundId.Monster_Death);
            QueenMushroom.rotAnglePerSecond = 0;
            QueenMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            QueenMushroom.CharacterisDead = true;

            if (DeadTime >= 1.2f)
            {
                QueenMushroom.OnDead();
                return;
            }
        }
    }
}
