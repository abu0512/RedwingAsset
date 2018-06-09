using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerShildRun : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen" || other.tag == "ShildMushroom" || other.tag == "EliteShaman")
        {
            CPlayerManager._instance.PlayerHitCamera(CCameraRayObj._instance.MaxDistanceValue, 0.2f);
            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else if (other.tag == "EliteShaman")
            {
                if(other.GetComponent<EliteShaman>().PlayerisFront == false)
                other.GetComponent<EliteShaman>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else if (other.tag == "ShildMushroom")
            {
                if(other.GetComponent<ShildMushroom>().PlayerisFront == false)
                other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else
            {
                other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }

            //CPlayerAttackEffect._instance.Effect8(); 이펙트
            CPlayerManager._instance._PlayerAni_Contorl.AniStiff();
        }
    }
}
