using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{ 
    public GameObject BossHP;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BossHP.SetActive(true);
            SoundManager.I.BossSoundPlay(Player.transform);
        }
    }
}
