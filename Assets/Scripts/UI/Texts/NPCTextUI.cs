using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NPCTextUI : MonoBehaviour
{
    [FormerlySerializedAs("ClickBlock")] public GameObject clickBlock;
    [FormerlySerializedAs("CharacterName")] public TMP_Text characterName;
    [FormerlySerializedAs("ChatText")] public TMP_Text chatText;
    [FormerlySerializedAs("TextPanel")] public GameObject textPanel;
    [FormerlySerializedAs("Image")] public GameObject[] imageArray;

    private string _writerText;
    private bool _skip;
    
    private static NPCTextUI _instance = null;
    private IEnumerator _fadeCoroutine = null;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        characterName.outlineWidth = 0.05f;
        chatText.outlineWidth = 0.05f;
        characterName.outlineColor = Color.white;
        chatText.outlineColor = Color.white;
        //textPanel.SetActive(false);
        clickBlock.SetActive(true);
    }
    
    public static NPCTextUI Instance => _instance == null ? null : _instance;
    
    public IEnumerator NormalChat(string narrator, string narration)
    {
        characterName.text = narrator;
        _writerText = "";
        _skip = false;

        if (narration.Length > 0)
        {
            textPanel.SetActive(true);
            clickBlock.SetActive(true);
        }
        else
        {
            textPanel.SetActive(false);
            clickBlock.SetActive(false);
            yield break;
        }
        
        foreach (var t in narration)
        {
            _writerText += t;
            chatText.text = _writerText;
            yield return new WaitForSeconds(0.1f);
            if (_skip)
                break;
        }

        chatText.text = narration;
        if (Time.timeScale != 0)
            Time.timeScale = 0;
    }

    public void StartCut1()
    {
        imageArray[1].SetActive(true);
    }

    public void EndCut1()
    {
        imageArray[1].SetActive(false);
    }
    
    public void StartCut2()
    {
        imageArray[2].SetActive(true);
        var animator = imageArray[2].transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("open");
    }

    public void EndCut2()
    {
        imageArray[2].SetActive(false);
    }

    public IEnumerator FadeInCoroutine(int index)
    {
        if (imageArray.Length < index) yield break;
        
        imageArray[index].SetActive(!imageArray[index].activeSelf);
        var numOfChild = imageArray[index].transform.childCount;
        for (var i = 0; i < numOfChild; i++)
        {
            _fadeCoroutine = Coroutine(index, i, true);
            StartCoroutine(_fadeCoroutine);
        }
    }
    
    public IEnumerator FadeOutCoroutine(int index)
    {
        if (imageArray.Length < index) yield break;

        var numOfChild = imageArray[index].transform.childCount;
        for (var i = 0; i < numOfChild; i++)
        {
            _fadeCoroutine = Coroutine(index, i, false);
            StartCoroutine(_fadeCoroutine);        
        }
        StartCoroutine(FadeUI.Instance.FadeInCoroutine());
    }

    private IEnumerator Coroutine(int index,int i,bool isIn)
    {
        var fadeCount = isIn ? 0.01f : 0.99f;
        var image = imageArray[index].transform.GetChild(i).GetComponent<Image>();

        if(isIn)
            image.raycastTarget = true;
        
        while (fadeCount is > 0.0f and < 1.0f)
        {
            fadeCount += isIn ? 0.01f : -0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            image.color = new Color(1, 1, 1, fadeCount);
        }

        if (isIn) yield break;
        imageArray[index].SetActive(false);
        image.raycastTarget = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _skip = true;
            if (Time.timeScale == 0)
                Time.timeScale = 1;
        }
    }
}
