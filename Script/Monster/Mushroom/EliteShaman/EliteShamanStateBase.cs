using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteShamanStateBase : MonoBehaviour {

    protected float Dltime;
    protected EliteShaman _EliteShaman;
    protected EliteShaman EliteShaman { get { return _EliteShaman; } set { _EliteShaman = value; } }

    private void Awake()
    {
        _EliteShaman = GetComponent<EliteShaman>();
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {
    }
}
