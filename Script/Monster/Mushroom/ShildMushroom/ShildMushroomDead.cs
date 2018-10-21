using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomDead : ShildMushroomStateBase
{
    ShildMushroomEffect _groggy;
    private float DeadTime;

    public override void BeginState()
    {
        _groggy = GetComponent<ShildMushroomEffect>();

        ShildMushroom.CharacterisDead = true;
        DeadTime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        if (ShildMushroom.isDead)
        {
            SoundManager.I.PlaySound(transform.position, PlaySoundId.Monster_Death);
            ShildMushroom.rotAnglePerSecond = 0;
            ShildMushroom.AttackRotAngle = 0;
            ShildMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;

            if (DeadTime >= 1.6f)
            {
                ShildMushroom.OnDead();
                return;
            }
        }
    }
}
