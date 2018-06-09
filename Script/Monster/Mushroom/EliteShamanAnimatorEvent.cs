using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanAnimatorEvent : MonoBehaviour {

    private EliteShaman _EliteShaman;

    private void Awake()
    {
        _EliteShaman = transform.GetComponent<EliteShaman>();
    }

    void QueenHitCheck()
    {
        EliteShamanAttack _EliteShamanAttaked = _EliteShaman.GetCurrentState() as EliteShamanAttack;
    }

    void QueenSHitCheck()
    {
        EliteShamanAttack2 _EliteShamanAttaked = _EliteShaman.GetCurrentState() as EliteShamanAttack2;
    }

    void EliteSHealing()
    {
        EliteShamanHealing _EliteShamanHeal = _EliteShaman.GetCurrentState() as EliteShamanHealing;
        if (_EliteShamanHeal != null)
        {
            _EliteShamanHeal.EliteSHealCheck();
        }
    }
}
