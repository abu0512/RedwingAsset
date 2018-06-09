using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomDead : GuardMushroomStateBase
{
    private float DeadTime;
    //private Vector3 _myposition;

    public override void BeginState()
    {
        DeadTime = 0;
        GuardMushroom.ExitGravity = true;
    }

    public override void EndState()
    {
        base.EndState();
    }

    //public void Mypo()
    //{
    //    _myposition = transform.position;
    //    _myposition.y += 0.5f;
    //    transform.position = _myposition;
    //}

    void Update()
    {
        if (GuardMushroom.isDead)
        {
            GuardMushroom.rotAnglePerSecond = 0;
            GuardMushroom.AttackRotAngle = 0;
            GuardMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            GuardMushroom.CharacterisDead = true;
            //Mypo();
            if (DeadTime >= 1.2f)
            {
                GuardMushroom.OnDead();
                return;
            }
        }
    }
}
