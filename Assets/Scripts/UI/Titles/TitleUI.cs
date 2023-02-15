using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject newGameButton;
    public GameObject exitButton;
    public GameObject credit;
    public GameObject[] button;
    public Sprite[] buttonImage;
    public GameObject warning;

    private void Start()
    {
        StartCoroutine(Warning());
    }

    IEnumerator Warning()
    {
        warning.SetActive(true);
        AudioMgr.Instance.bgmSound.Stop();
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        AudioMgr.Instance.bgmSound.clip = AudioMgr.Instance.bgmClip[0];
        AudioMgr.Instance.bgmSound.Play();
        warning.SetActive(false);
    }

    #region Menu
    public void OptionOpen()
    {
        optionPanel.SetActive(true);
        AudioMgr.Instance.ButtonClick();
    }
    public void NewGame()
    {
        newGameButton.SetActive(true);
        AudioMgr.Instance.ButtonClick();
    }
    public void NewGameStart()
    {
        GameMgr.Instance.ChangeScene("Scenes/Story1Scene");
    }
    public void NewGameClose()
    {
        newGameButton.SetActive(false);
    }
    public void NewGameOn()
    {
        button[0].GetComponent<Image>().sprite = buttonImage[1];
    }
    public void NewGameOff()
    {
        button[0].GetComponent<Image>().sprite = buttonImage[0];
    }
    public void ContinueOn()
    {
        button[1].GetComponent<Image>().sprite = buttonImage[3];
    }
    public void ContinueOff()
    {
        button[1].GetComponent<Image>().sprite = buttonImage[2];
    }
    public void Continue()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            switch (PlayerPrefs.GetInt("Level"))
            {
                case 1:
                    GameMgr.Instance.ChangeScene("Scenes/Story1Scene");
                    break;
                case 2:
                    GameMgr.Instance.ChangeScene("Scenes/Story2Scene");
                    break;
                case 3:
                    GameMgr.Instance.ChangeScene("Scenes/Story3Scene");
                    break;
                case 4:
                    GameMgr.Instance.ChangeScene("Scenes/Story4Scene");
                    break;
            }
        }
        else
        {
            NewGameStart();
        }
    }
    public void SettingOn()
    {
        button[2].GetComponent<Image>().sprite = buttonImage[5];
    }
    public void SettingOff()
    {
        button[2].GetComponent<Image>().sprite = buttonImage[4];
    }
    public void CreditOn()
    {
        button[3].GetComponent<Image>().sprite = buttonImage[7];
    }
    public void CreditOff()
    {
        button[3].GetComponent<Image>().sprite = buttonImage[6];
    }
    public void Credit()
    {
        credit.SetActive(true);
    }
    public void CreditClose()
    {
        credit.SetActive(false);
    }
    public void ExitStart()
    {
        exitButton.SetActive(true);
        AudioMgr.Instance.ButtonClick();
    }
    public void ExitClose()
    {
        exitButton.SetActive(false);
    }
    public void ExitOn()
    {
        button[4].GetComponent<Image>().sprite = buttonImage[9];
    }
    public void ExitOff()
    {
        button[4].GetComponent<Image>().sprite = buttonImage[8];
    }
    public void OutGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion Menu
}
