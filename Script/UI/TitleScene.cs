using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour {

    [Header("Title")]
    public Image fade;
    float fades = 1.0f;
    float time = 0;
    bool isinputanykey = false;
    bool fadeinfinish = false;

    AudioSource TitleAudio;

    private void TitleStartFadein()
    {
        if (!fadeinfinish)
        {
            time += Time.deltaTime;
            if (fades > 0f && time >= 0.1f)
            {
                fades -= 0.25f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            if (fades <= 0f)
            {
                fadeinfinish = true;
            }
        }
    }

    private void TitleExitFadeOut()
    {
        if (isinputanykey)
        {
            time += Time.deltaTime;
            if (fades < 1f && time >= 0.1f)
            {
                fades += 0.25f;
                fade.color = new Color(0, 0, 0, fades);
                time = 0;
            }

            if (fades >= 0.91f)
            {
                SceneManager.LoadSceneAsync("Intro");
            }
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        TitleAudio = GetComponent<AudioSource>();
        TitleAudio.Play();
    }

    void Update ()
    {
        // Fade in / Out Set
        TitleStartFadein();
        TitleExitFadeOut();

        if (Input.anyKey)
        {
            isinputanykey = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
