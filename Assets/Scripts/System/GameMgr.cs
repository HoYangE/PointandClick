using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    private static GameMgr _instance = null;
    private bool _isPause = false;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameMgr Instance => _instance == null ? null : _instance;

    #region SceneSystem

    private IEnumerator _coroutine;
    public void ChangeScene(string sceneName)
    {
        _coroutine = FadeUI.Instance.FadeOutCoroutine(sceneName, true);
        StartCoroutine(_coroutine);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        ChangeScene("Scenes/TitleScene");
    }
    #endregion SceneSystem
    
    
    #region PauseSystem
    public void PauseGame()
    {
        if (_isPause == false)
        {
            Time.timeScale = 0;
            _isPause = true;
        }
        else
        {
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _isPause = false;
        return;
    }
    #endregion

}
