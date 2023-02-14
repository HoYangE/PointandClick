using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;

    public void OpenMenu()
    {
        AudioMgr.Instance.ButtonClick();
        menu.SetActive(true);
    }
    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void Setting()
    {
        AudioMgr.Instance.ButtonClick();
        AudioMgr.Instance.OptionOn();
    }

    public void Main()
    {
        AudioMgr.Instance.ButtonClick();
        Destroy(setting);
        GameMgr.Instance.ChangeScene("Scenes/TitleScene");
    }
    public void OutGame()
    {
        Destroy(setting);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
