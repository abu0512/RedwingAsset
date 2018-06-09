using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanGroggy : EliteShamanStateBase
{
    Vector3 SavePosition;
    EliteShamanEffect _groggy;

    public override void BeginState()
    {
        base.BeginState();
        _groggy = GetComponent<EliteShamanEffect>();
        Dltime = 0f;
        SavePosition = transform.position;
    }

    public override void EndState()
    {
        base.EndState();
    }


    void Update()
    {
        _groggy.Groggy(transform.position);
        Dltime += Time.deltaTime;
        EliteShaman.GoToDestination(SavePosition, 0, 0);

        if (Dltime > 5f)
        {
            EliteShaman.SetState(EliteShamanState.Return);
            _groggy.GroggyEffect.SetActive(false);
            return;
        }
    }
}
