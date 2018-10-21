using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour {

    public VideoClip movie;
    public Camera camera;
    public Image Set;
    private RawImage Image;
    private VideoPlayer IntroMovie;
    private AudioSource audio;

    [Header("Skip")]
    public GameObject ifSkip;
    public Image fade;
    public bool Skip = false;
    float fades = 0f;
    float time = 0;
    float stack = 0;



    void Awake()
    {
        Set.enabled = true;
        Image = GetComponent<RawImage>();
        IntroMovie = gameObject.AddComponent<VideoPlayer>();
        audio = gameObject.AddComponent<AudioSource>();
        IntroMovie.playOnAwake = false;
        audio.playOnAwake = false;
        audio.Pause();
        PlayVideo();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FadeSet()
    {
        if (Skip)
        {
            time += Time.deltaTime;
            if (fades < 0.99f && time >= 0.1f)
            {
                fades += 0.06f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            else if (fades >= 0.98f && stack == 0)
            {
                stack++;
                SceneManager.LoadSceneAsync("ABU_3");
            }
        }
    }

    public void PlayVideo()

    {
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()

    {
        IntroMovie.source = VideoSource.VideoClip;
        IntroMovie.clip = movie;
        IntroMovie.renderMode = VideoRenderMode.CameraNearPlane;
        IntroMovie.targetCamera = camera;
        IntroMovie.audioOutputMode = VideoAudioOutputMode.AudioSource;
        IntroMovie.EnableAudioTrack(0, true);
        IntroMovie.SetTargetAudioSource(0, audio);
        IntroMovie.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);
        while (!IntroMovie.isPrepared)
        {
            Debug.Log("동영상 준비중...");
            yield return waitTime;
        }
        Debug.Log("동영상이 준비가 끝났습니다.");
        //Image.texture = IntroMovie.texture;
        IntroMovie.Play();
        Set.enabled = false;
        audio.Play();
        Debug.Log("동영상이 재생됩니다.");
        while (IntroMovie.isPlaying)
        {
            Debug.Log("동영상 재생 시간 : " + Mathf.FloorToInt((float)IntroMovie.time));
            yield return null;
        }
        Debug.Log("영상이 끝났습니다.");
        SceneManager.LoadSceneAsync("ABU_3");
    }

    private void FixedUpdate()
    {
        //Skip할 경우 세팅
        FadeSet();
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            IntroMovie.Pause();
            Skip = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
