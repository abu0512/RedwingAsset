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
    Boss_Orb,
    Hit_Pc,
    Walk_DealerStone,
    Walk_DealerGrass,
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager I { get { return _instance; } }

    [FMODUnity.EventRef]
    public string[] Sounds;

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
        bgmSound = FMODUnity.RuntimeManager.CreateInstance(Sounds[(int)PlaySoundId.Bgm1]);
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
        FMODUnity.RuntimeManager.PlayOneShot(Sounds[(int)id], target);
    }
}