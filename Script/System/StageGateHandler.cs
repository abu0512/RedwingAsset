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
        foreach (StageGate g in _gates)
        {
            g.SetGateState(flag);
        }
    }
}
