using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public GameObject optionPanel;

    public void StartGame()
    {
        GameMgr.Instance.ChangeScene("Scenes/Story1Scene");
    }
    public void OptionOpen()
    {
        optionPanel.SetActive(true);
    }
    public void OptionClose()
    {
        optionPanel.SetActive(false);
    }
    public void OutGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
