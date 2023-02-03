using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public Image image;
    
    private static FadeUI _instance = null;

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
        image.color = new Color(0, 0, 0, 0);
    }
    
    private void Start()
    {
        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    public static FadeUI Instance => _instance == null ? null : _instance;

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInCoroutine());
    }

    #region FadeSystem
    public IEnumerator FadeInCoroutine()
    {
        var fadeCount = 1.0f;
        GameMgr.Instance.PauseGame();
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        GameMgr.Instance.ContinueGame();
    }
    public IEnumerator FadeOutCoroutine(string sceneName, bool isLoadScene)
    {
        var fadeCount = 0.0f;
        GameMgr.Instance.PauseGame();
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        GameMgr.Instance.ContinueGame();
        if(isLoadScene)
            SceneManager.LoadScene(sceneName);
    }
    #endregion FadeSystem
}
