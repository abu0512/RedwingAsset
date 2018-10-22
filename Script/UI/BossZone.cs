using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{ 
    public GameObject BossHP;
    public GameObject Player;
    public GameObject TipOff;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BossHP.SetActive(true);
            TipOff.SetActive(false);
            SoundManager.I.BossSoundPlay(Player.transform);
        }
    }
}
