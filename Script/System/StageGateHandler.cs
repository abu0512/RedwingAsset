using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGateHandler : MonoBehaviour
{
    private StageGate[] _gates;


    private void Awake()
    {
        _gates = FindObjectsOfType<StageGate>();
        SetGateState(false);
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {

    }

    public void SetGateState(bool flag)
    {
        // 나중에 게이트 쉐이더 쓰는걸로 바꿔야댐
        foreach (StageGate g in _gates)
        {
            g.gameObject.SetActive(flag);
        }
    }
}
