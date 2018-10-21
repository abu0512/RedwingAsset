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
    Boss_Release,
    Boss_FootHold,
    Boss_Arrow,
    Boss_Arrow_Spawn,
    Boss_Orb,
    Hit_Pc,
    Walk_DealerStone,
    Walk_DealerGrass,
    Tanker_Dash,
    Dealer_Blink,
    Dealer_QuickCut,
    Tanker_Knockback,
    Dealer_Holding,
    Boss_Walk,
    Boss_Teleport,
    Boss_Attack1,
    Boss_Attack2,
    Boss_Attack3,
    Boss_MonSpawn,
    Tanker_Swap,
    Dealer_Swap,
    Tanker_Rush,
    Tanker_Defense,
    Vine,
    Vine_Fast,
    Monster_Death,
    Goblin_Missile,
    Goblin_Heal,
    ShildGoblin_Hit,
    GuardGoblin_AttackHit,
    ShildGoblin_AttackHit,
    GuardGoblin_Sword,
    ShildGoblin_Sword
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
    private static SoundManager _instance;
    public static SoundManager I { get { return _instance; } }

    public PlaySoundType[] Sounds;
    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;

    public float _Volume;

    void Awake()
    {
        _instance = this;
        _Volume = 0;
    }

    public void Update()
    {
        bgmVolume.setValue(_Volume);
    }


    public void SoundPlay(Transform Target)
    {
        bgmSound = FMODUnity.RuntimeManager.CreateInstance(GetSound(PlaySoundId.Bgm1));
        bgmSound.getParameter("Parameter 1", out bgmVolume);
        //FMODUnity.RuntimeManager.PlayOneShot(MyEvent1[SoundType], Target.position);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmSound, Target, GetComponent<Rigidbody>());
        bgmSound.start();
    }

    public void PlaySound(Transform target, PlaySoundId id)
    {
        PlaySound(target.position, id);
    }

    public void PlaySound(Vector3 target, PlaySoundId id)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GetSound(id), target);
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