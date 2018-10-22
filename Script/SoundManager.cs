using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaySoundId
{
    Bgm1 = 0,
    Walk_TankerStone,
    Walk_TankerGrass,
    Attack_Original,
    Attack_Scythe,
    Attack_Counter,
    Defense_Shield,
    Skill_ScytheWideCut,
    Hit_StandardMonster,
    Attack_Finish,
    Boss_Release = 10,
    Boss_FootHold,
    Boss_Arrow,
    Boss_Arrow_Spawn,
    Boss_Orb,
    Boss_Orb_Spawn,
    Hit_Pc,
    Walk_DealerStone,
    Walk_DealerGrass,
    Tanker_Dash,
    Dealer_Blink = 20,
    Dealer_QuickCut,
    Tanker_Knockback,
    Dealer_Holding,
    Boss_Walk,
    Boss_Teleport,
    Boss_Attack1,
    Boss_Attack2,
    Boss_Attack3,
    Boss_MonSpawn,
    Tanker_Swap = 30,
    Dealer_Swap,
    Tanker_Rush,
    Tanker_Defense,
    Vine,
    Vine_Fast,
    Monster_Death,
    Goblin_Missile,
    Goblin_Heal,
    ShildGoblin_Hit,
    GuardGoblin_AttackHit = 40,
    ShildGoblin_AttackHit,
    GuardGoblin_Sword,
    ShildGoblin_Sword,
    Bgm2,
    Boss_Groggy,
    Tanker_Rush_Hit,
}

[Serializable]
public struct PlaySoundType
{
    public PlaySoundId Id;
    [FMODUnity.EventRef]
    public string Path;
}

public class SoundManager : MonoBehaviour
{
    public GameObject isBossZone;
    private static SoundManager _instance;
    public static SoundManager I { get { return _instance; } }

    public PlaySoundType[] Sounds;
    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.EventInstance BossbgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;

    public float _Volume;

    void Awake()
    {
        _instance = this;
        _Volume = 0;
        SoundPlay(CPlayerManager._instance.transform);
    }

    public void Update()
    {
        bgmVolume.setValue(_Volume);

        if (isBossZone.activeInHierarchy)
            SoundPlay(CPlayerManager._instance.transform);
    }


    public void SoundPlay(Transform Target)
    {
        if (isBossZone.activeInHierarchy)
            bgmSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        else
        {
            bgmSound = FMODUnity.RuntimeManager.CreateInstance(GetSound(PlaySoundId.Bgm1));
            bgmSound.getParameter("Parameter 1", out bgmVolume);
            //FMODUnity.RuntimeManager.PlayOneShot(MyEvent1[SoundType], Target.position);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmSound, Target, GetComponent<Rigidbody>());
            bgmSound.start();
        }
    }

    public void BossSoundPlay(Transform Target)
    {
        BossbgmSound = FMODUnity.RuntimeManager.CreateInstance(GetSound(PlaySoundId.Bgm2));
        BossbgmSound.getParameter("Parameter 1", out bgmVolume);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(BossbgmSound, Target, GetComponent<Rigidbody>());
        BossbgmSound.start();
    }

    public void PlaySound(Transform target, PlaySoundId id)
    {
        PlaySound(target.position, id);
    }

    public void PlaySound(Vector3 target, PlaySoundId id)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GetSound(id), CPlayerManager._instance.transform.position);
    }

    private string GetSound(PlaySoundId id)
    {
        foreach (PlaySoundType t in Sounds)
        {
            if (t.Id == id)
                return t.Path;
        }

        return "";
    }
}