﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageVine : MonoBehaviour
{
    public float MaxGrowth;
    public float MinGrowth;

    private Material _mat;

    private float _growth;
    private float _speed;
    private bool _isOn;

    // properties
    public float Growth { get { return _growth; } }

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _growth = MaxGrowth;
        _isOn = false;
        _speed = 2.0f;
    }

	void Start ()
    {
	    	
	}
	
	void Update ()
    {
        GrowthOffUpdate();
        GrowthOnUpdate();

    }

    private void GrowthOffUpdate()
    {
        if (_isOn)
            return;

        _growth -= _speed * Time.deltaTime;
        _mat.SetFloat("_growth", _growth);

        if (_growth <= MinGrowth)
            _growth = MinGrowth;
    }

    private void GrowthOnUpdate()
    {
        if (!_isOn)
            return;

        _growth += _speed * Time.deltaTime;
        _mat.SetFloat("_growth", _growth);

        if (_growth >= MaxGrowth)
            _growth = MaxGrowth;
    }

    public void SetVineState(bool flag)
    {
        _isOn = flag;
    }
}
