using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromCheck : MonoBehaviour {

    EliteShaman _eliteShaman;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _eliteShaman = GetComponentInParent<EliteShaman>();
    }

    public float GetDistanceFromPlayer() // Player 캐릭터와 거리를 되돌려줄 함수
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        return distance;
    }

    public void isbattle()
    {
        
    }

	void Update () {
		
	}
}
