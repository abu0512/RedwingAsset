using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{ 
    public GameObject BossHP;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            BossHP.SetActive(true);
    }
}
