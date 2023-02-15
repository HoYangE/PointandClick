using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story4 : MonoBehaviour
{
    public GameObject backButton;

    public Sprite[] imageArray;
    public GameObject eventImage;
    public GameObject eventInteraction;

    public GameObject titleButton;
    public GameObject quitButton;

    void Start()
    {
        StartCoroutine(Talk());
        if (Inventory.Instance.FindItem("ChildDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[2]);
        if (Inventory.Instance.FindItem("ParentDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[11]);
        if (Inventory.Instance.FindItem("PassageDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[8]);
        PlayerPrefs.SetInt("Level", 4);
        AudioMgr.Instance.bgmSound.clip = AudioMgr.Instance.bgmClip[1];
        AudioMgr.Instance.bgmSound.Play();
    }

    IEnumerator Talk()
    {
        var data = CSVReader.Read("Story4TextScript");
        foreach (var t in data)
        {
            switch (int.Parse(t["Talk"].ToString()))
            {
                case 1:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(0));
                    break;

                case 4:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(0));
                    break;
            }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
        }
    }

    IEnumerator ClearTalk()
    {
        var data = CSVReader.Read("Story4EndTextScript");
        foreach (var t in data)
        {
            switch (int.Parse(t["Talk"].ToString()))
            {
                case 1:
                    yield return new WaitForSeconds(0.5f);
                    Dolls.GetComponent<Animator>().SetTrigger("isBurn");
                    yield return new WaitForSeconds(3f);
                    StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(1));
                    yield return new WaitForSeconds(3f);
                    break;

                case 4:
                    backButton.SetActive(false);
                    Inventory.Instance.gameObject.SetActive(false);
                    NPCTextUI.Instance.imageArray[2].SetActive(true);
                    yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(1));
                    break;

                case 8:
                    yield return new WaitForSeconds(1f);
                    StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(3));
                    break;
            }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
        }

        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        NPCTextUI.Instance.imageArray[4].SetActive(true);
        StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(3));
        yield return new WaitForSeconds(1f);
        quitButton.SetActive(true);
        titleButton.SetActive(true);
    }

    #region Event

    public void EventPopUp(int eventNumber)
    {
        eventImage.SetActive(true);
        backButton.SetActive(true);
        eventImage.GetComponent<Image>().sprite = imageArray[eventNumber];
        eventInteraction.transform.GetChild(eventNumber).gameObject.SetActive(true);
        Dolls.SetActive(true);
    }

    public void CloseEvent()
    {
        foreach (Transform child in eventInteraction.transform)
        {
            child.gameObject.SetActive(false);
        }

        eventImage.SetActive(false);
        backButton.SetActive(false);
        Dolls.SetActive(false);
    }

    #region Ground

    public GameObject Dolls;
    bool _setDoll1;
    bool _setDoll2;
    bool _setDoll3;

    public void SetDoll1()
    {
        if (!_setDoll1)
        {
            _setDoll1 = true;
            AudioMgr.Instance.Put_object();
            Inventory.Instance.RemoveDoll(Inventory.Instance.FindItem("ChildDoll1"));
            Dolls.transform.GetChild(0).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            CheckDoll();
        }

    }

    public void SetDoll2()
    {
        if (!_setDoll2)
        {
            _setDoll2 = true;
            Inventory.Instance.RemoveDoll(Inventory.Instance.FindItem("ParentDoll1"));
            AudioMgr.Instance.Put_object();
            Dolls.transform.GetChild(2).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            CheckDoll();
        }
    }

    public void SetDoll3()
    {
        if (!_setDoll3)
        {
            _setDoll3 = true;
            Inventory.Instance.RemoveDoll(Inventory.Instance.FindItem("PassageDoll1"));
            AudioMgr.Instance.Put_object();
            Dolls.transform.GetChild(1).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            CheckDoll();
        }
    }

    public void CheckDoll()
    {
        foreach (Transform child in Dolls.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                return;
            }
        }

        NPCTextUI.Instance.clickBlock.SetActive(true);
        StartCoroutine(ClearTalk());
    }
    #endregion Ground

    #endregion Event

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void GoTitle()
    {
        GameMgr.Instance.ChangeScene("Scenes/TitleScene");
    }
}
