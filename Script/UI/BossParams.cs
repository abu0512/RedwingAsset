using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossParams : CharacterUI
{
    public string names { get; set; }

    public Image BMHPBar;
    public Image BMSHPBar;

    private float WaitSec;
    MonsterStat _Boss;

    public override void InitParams()
    {
        names = "BossMonster";
        maxHP = _Boss.MaxHp;
        curHP = _Boss.MaxHp;
        saveHP = _Boss.MaxHp; ;
    }

    private void Awake()
    {
        WaitSec = 0;
        _Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<MonsterStat>();
    }

    public void SetHp()
    {
        curHP = _Boss.Hp;
        curHP = Mathf.Clamp(curHP, 0, maxHP);

    }

    public void SetSHp()
    {
        if (saveHP > curHP)
        {
            WaitSec += Time.fixedDeltaTime;

            if (WaitSec > 1.2f)
            {
                saveHP -= 400 * Time.deltaTime;
                if (saveHP < curHP)
                {
                    saveHP = curHP;
                }
            }
        }

        if (curHP == saveHP)
        {
            WaitSec = 0;
        }
    }

    public void HPlocalScale()
    {
        float _hp = curHP / maxHP;
        BMHPBar.fillAmount = curHP / maxHP;
    }

    public void SHPlocalScale()
    {
        float _shp = saveHP / maxHP;
        BMSHPBar.fillAmount = saveHP / maxHP;
    }

    private void FixedUpdate()
    {
        SetSHp();
    }

    void Update()
    {
        // Boss 몬스터 체력 불러오기
        SetHp();

        // Boss 캐릭터 HP bar 실시간 UI 상태 변화
        HPlocalScale();
        SHPlocalScale();

        if (curHP <= 0)
        {
            _Boss.gameObject.SetActive(false);
        }
    }
}