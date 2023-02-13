using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject setting;

    public void OpenMenu()
    {
        menu.SetActive(true);
    }
    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void Setting()
    {
        AudioMgr.Instance.OptionOn();
    }

    public void Main()
    {
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
