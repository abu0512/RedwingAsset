using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static PlayerParams _instance = null;

    protected CPlayerManager _CPlayerManager;
    protected CPlayerManager CPlayerManager { get { return _CPlayerManager; } set { _CPlayerManager = value; } }

    // public GameObject BossParams;

    [Header("Pause")]
    // Puase 관련 변수들
    public GameObject GamePasueUI;
    public Image[] PauseLogo;
    public Image[] ChooseRestart;
    public Image[] ChooseExit;
    private bool isGamePause = false;
    private bool isPauseSelected = false;
    private int PauseState = 0;

    [Header("Die")]
    // Game Over 관련 변수들
    public GameObject GameOverUI;
    public GameObject StandBy;
    public Image[] DieLogo;
    public Image[] ChooseYes;
    public Image[] ChooseNo;
    public Image fade;
    private int DieState = 0;
    private bool isDieSelected = false;
    private bool isGameOver = false;
    float fades = 0f;
    float time = 0;

    private void GameOverFadein()
    {
        GameOverUI.SetActive(true);
        time += Time.deltaTime;
        if (fades < 0.91f && time >= 0.1f)
        {
            fades += 0.07f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }

        else if (fades >= 0.9f)
        {
            isGameOver = true;
            StandBy.SetActive(true);
        }
    }

    void GamePauseSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isGamePause = !isGamePause;
            GamePasueUI.SetActive(true);
    }

    void GameOverSet()
    {
        if (CPlayerManager._instance.m_PlayerHp <= 0)
        {
            GameOverFadein();
        }
    }

    void GamePause()
    {
        if (!isGamePause)
        {
            GamePasueUI.SetActive(false);
            Time.timeScale = 1f;
        }

        if (isGamePause)
        {
            Time.timeScale = 0;
        }

        if(!isPauseSelected)
        {
            ChooseRestart[0].enabled = ChooseRestart[1].enabled = true;
            ChooseExit[0].enabled = ChooseExit[1].enabled = false;
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            PauseState++;
            isPauseSelected = true;
            if (PauseState == 2)
                PauseState = 0;

            switch (PauseState)
            {
                case 0:
                    ChooseRestart[0].enabled = ChooseRestart[1].enabled = true;
                    ChooseExit[0].enabled = ChooseExit[1].enabled = false;
                    break;
                case 1:
                    ChooseExit[0].enabled = ChooseExit[1].enabled = true;
                    ChooseRestart[0].enabled = ChooseRestart[1].enabled = false;
                    break;
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            switch (PauseState)
            {
                case 0:
                    SceneManager.LoadSceneAsync("JJTitle");
                    break;
                case 1:
                    isGamePause = false;
                    break;
            }
        }
    }

    void GameOver()
    {
        if(!isDieSelected && isGameOver)
        {
            ChooseYes[0].enabled = ChooseYes[1].enabled = true;
            ChooseNo[0].enabled = ChooseNo[1].enabled = false;
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            isDieSelected = true;
            DieState++;
            if (DieState == 2)
                DieState = 0;

            switch (DieState)
            {
                case 0:
                    ChooseYes[0].enabled = ChooseYes[1].enabled = true;
                    ChooseNo[0].enabled = ChooseNo[1].enabled = false;
                    break;
                case 1:
                    ChooseNo[0].enabled = ChooseNo[1].enabled = true;
                    ChooseYes[0].enabled = ChooseYes[1].enabled = false;
                    break;
            }
        }

        if (Input.GetButtonDown("Submit"))
        { 
            switch (DieState)
            {
                case 0:
                    SceneManager.LoadSceneAsync("JJTest");
                    break;
                case 1:
                    SceneManager.LoadSceneAsync("JJTitle");
                    break;
            }
        }
    }

    void Start()
    {
        StandBy.SetActive(false);
        GameOverUI.SetActive(false);
        GamePasueUI.SetActive(false);
    }

    void Update()
    {  //BossParamsSet();
        GamePauseSet();
        GamePause();
        GameOverSet();
        if (isGameOver)
        {
            if (DieState >= 0)
                GameOver();
            return;
        }
    }
}
