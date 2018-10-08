using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour {

    public VideoClip movie;
    public Camera camera;
    public Image Set;
    private RawImage Image;
    private  VideoPlayer EndingMovie;
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
        EndingMovie = gameObject.AddComponent<VideoPlayer>();
        audio = gameObject.AddComponent<AudioSource>();
        EndingMovie.playOnAwake = false;
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
            if (fades < 0.94f && time >= 0.1f)
            {
                fades += 0.08f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            else if (fades >= 0.93f && stack == 0)
            {
                SceneManager.LoadSceneAsync("JJTitle");
            }
        }
    }

    public void PlayVideo()

    {
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()

    {
        EndingMovie.source = VideoSource.VideoClip;
        EndingMovie.clip = movie;
        EndingMovie.renderMode = VideoRenderMode.CameraNearPlane;
        EndingMovie.targetCamera = camera;
        EndingMovie.audioOutputMode = VideoAudioOutputMode.AudioSource;
        EndingMovie.EnableAudioTrack(0, true);
        EndingMovie.SetTargetAudioSource(0, audio);
        EndingMovie.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);
        while (!EndingMovie.isPrepared)
        {
            Debug.Log("동영상 준비중...");
            yield return waitTime;
        }
        Debug.Log("동영상이 준비가 끝났습니다.");
        //Image.texture = EndingMovie.texture;
        EndingMovie.Play();
        Set.enabled = false;
        audio.Play();
        Debug.Log("동영상이 재생됩니다.");
        while (EndingMovie.isPlaying)
        {
            Debug.Log("동영상 재생 시간 : " + Mathf.FloorToInt((float)EndingMovie.time));
            yield return null;
        }
        Debug.Log("영상이 끝났습니다.");
        SceneManager.LoadSceneAsync("JJTitle");
    }
	
	void Update () {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            ifSkip.SetActive(false);
            Skip = true;
        }

        FadeSet();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
