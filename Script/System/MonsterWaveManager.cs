using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveManager : Singleton<MonsterWaveManager>
{
    //private static MonsterWaveManager _instance;
    //public static MonsterWaveManager I { get { return _instance; } }

    private List<StageArea> _stages = new List<StageArea>();
    private int _curStage;
    private bool _isRun;
    private bool _isStart;

    public bool IsRun { get { return _isRun; } }
    public int CurrentStage { get { return _curStage; } }

    private void Awake()
    {
        //_instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StageArea area = transform.GetChild(i).GetComponent<StageArea>();
            area.gameObject.SetActive(true);
            _stages.Add(area);
        }

        _curStage = 0;
    }

    void Update()
    {
        if (!_isStart)
            return;

        if (_curStage >= _stages.Count)
            return;

        if (_stages[_curStage].StartNextWave() == AreaState.End)
        {
            _isRun = false;
            FindObjectOfType<StageGateHandler>().SetGateState(false);
        }
        else
            _isRun = true;
    }

    public void AddStage()
    {
        if (!_isStart)
            return;

        _curStage++;

        if (_curStage >= _stages.Count)
            return;

        _stages[_curStage].StartWave();
    }

    public void StartWave()
    {
        if (_isStart)
            return;

        _isStart = true;
        _stages[_curStage].StartWave();
    }
}
