using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class Story2 : MonoBehaviour
{
    public GameObject eventManager;
    public GameObject backButton;

    public Sprite[] imageArray;
    public GameObject eventImage;
    public GameObject eventInteraction;

    public GameObject virtualCamera;

    public void Start()
    {
        StartCoroutine(Talk());
        SetTime();
        if (Inventory.Instance.FindItem("ChildDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[2]);
        PlayerPrefs.SetInt("Level", 2);
    }

    #region Start

    IEnumerator Talk()
    {
        var data = CSVReader.Read("Story2TextScript");
        foreach (var t in data)
        {
            // switch (int.Parse(t["Talk"].ToString()))
            // {
            //     case 1:
            //         yield return StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(0));
            //         break;
            //     case 4:
            //         yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(0));
            //         break;
            // }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
    }

    IEnumerator ClearTalk()
    {
        var data = CSVReader.Read("Story2ClearTextScript");
        foreach (var t in data)
        {
            switch (int.Parse(t["Talk"].ToString()))
            {
                case 1:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(1));
                    break;
                case 9:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(1));
                    break;
            }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
        Inventory.Instance.AllRemoveItem();
        GameMgr.Instance.ChangeScene("Scenes/Story3Scene");

    }
    #endregion

    #region Event

    public void EventPopUp(int eventNumber)
    {
        eventImage.SetActive(true);
        backButton.SetActive(true);
        eventImage.GetComponent<Image>().sprite = imageArray[eventNumber];
        eventInteraction.transform.GetChild(eventNumber).gameObject.SetActive(true);
    }

    public void CloseEvent()
    {
        foreach(Transform child in eventInteraction.transform)
        {
            child.gameObject.SetActive(false);
        }

        eventImage.SetActive(false);
        backButton.SetActive(false);
        _drawer1Open = false;
        _drawer2Open = false;
    }

    #region Drawer
    bool _drawer1Open;
    bool _drawer2Open;
    bool _caseOpen;
    bool _firstOpen;

    public TextMeshProUGUI Lock1;
    public TextMeshProUGUI Lock2;
    public TextMeshProUGUI Lock3;
    public TextMeshProUGUI Lock4;

    public void Drawer1()
    {
        if (_drawer2Open)
        {
            eventImage.GetComponent<Image>().sprite = imageArray[8];
            eventInteraction.transform.GetChild(6).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(7).gameObject.SetActive(true);
        }
        else
        {
            eventImage.GetComponent<Image>().sprite = imageArray[6];
            eventInteraction.transform.GetChild(6).gameObject.SetActive(true);
        }

        _drawer1Open = true;
    }

    public void Drawer2()
    {
        if (!_haskey)
        {
            return;
        }
        else if (_drawer1Open && !_firstOpen)
        {
            _firstOpen = true;
            Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("ParentKey"));
            eventImage.GetComponent<Image>().sprite = imageArray[8];
            eventInteraction.transform.GetChild(6).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(7).gameObject.SetActive(true);
        }
        else if (!_drawer1Open && !_firstOpen)
        {
            _firstOpen = true;
            Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("ParentKey"));
            eventImage.GetComponent<Image>().sprite = imageArray[7];
            eventInteraction.transform.GetChild(7).gameObject.SetActive(true);
        }
        else if(_drawer1Open && _firstOpen)
        {
            eventImage.GetComponent<Image>().sprite = imageArray[8];
            eventInteraction.transform.GetChild(6).gameObject.SetActive(true);
            eventInteraction.transform.GetChild(7).gameObject.SetActive(true);
        }
        else
        {
            eventImage.GetComponent<Image>().sprite = imageArray[7];
            eventInteraction.transform.GetChild(7).gameObject.SetActive(true);
        }

        _drawer2Open = true;
    }

    public void Card()
    {
        eventInteraction.transform.GetChild(0).gameObject.SetActive(false);
        eventImage.GetComponent<Image>().sprite = imageArray[9];
    }

    public void PencilCase()
    {
        if (!_caseOpen)
        {
            eventInteraction.transform.GetChild(0).gameObject.SetActive(false);
            eventInteraction.transform.GetChild(8).gameObject.SetActive(true);
            eventImage.GetComponent<Image>().sprite = imageArray[10];
        }

    }

    int _lock1 = 0;
    int _lock2 = 0;
    int _lock3 = 0;
    int _lock4 = 0;
    public void PencilCaseLock1()
    {
        if(_lock1 < 9)
        {
            _lock1++;
        }
        else
        {
            _lock1 = 0;
        }
        //_lock1 = _lock1 < 9 ? _lock1++ : _lock1 = 0;
        Lock1.text = _lock1.ToString();
        CheckPassword();
    }
    public void PencilCaseLock2()
    {
        if (_lock2 < 9)
        {
            _lock2++;
        }
        else
        {
            _lock2 = 0;
        }
        Lock2.text = _lock2.ToString();
        CheckPassword();
    }
    public void PencilCaseLock3()
    {
        if (_lock3 < 9)
        {
            _lock3++;
        }
        else
        {
            _lock3 = 0;
        }
        Lock3.text = _lock3.ToString();
        CheckPassword();
    }
    public void PencilCaseLock4()
    {
        if (_lock4 < 9)
        {
            _lock4++;
        }
        else
        {
            _lock4 = 0;
        }
        Lock4.text = _lock4.ToString();
        CheckPassword();
    }

    public void CheckPassword()
    {
        if(int.Parse(Lock1.text) == 4 && int.Parse(Lock2.text) == 3 && int.Parse(Lock3.text) == 2 && int.Parse(Lock4.text) == 1)
        {
            _caseOpen = true;
            _hasEraser = true;
            Inventory.Instance.AddItem(Inventory.Instance.imageList[10]);

            CloseEvent();
        }
    }

    #endregion Drawer

    #region Closet
    public GameObject _horrorHand;
    public GameObject _key;
    bool _popUpHand;
    bool _haskey;

    public void Closet()
    {

    }

    public void Key()
    {
        if (!_popUpHand)
        {
            _horrorHand.SetActive(true);
            _horrorHand.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            _popUpHand = true;
            StartCoroutine(HorrorHand());
            virtualCamera.GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.8f;
        }
        else
        {
            _haskey = true;
            Inventory.Instance.AddItem(Inventory.Instance.imageList[9]);
            Destroy(_key);
        }
    }
    private IEnumerator HorrorHand()
    {
        yield return new WaitForSeconds(0.3f);
        _horrorHand.SetActive(false);
        virtualCamera.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
    }


#endregion Closet

    #region Table
    bool _hasEraser;
    bool _isErased;

    public void DollManual()
    {
        if (_isErased)
        {
            eventImage.GetComponent<Image>().sprite = imageArray[12];
        }
        else
        {
            eventInteraction.transform.GetChild(9).gameObject.SetActive(true);
            eventImage.GetComponent<Image>().sprite = imageArray[11];
        }
    }

    public void EraseDollManual()
    {
        if(_hasEraser && !_isErased)
        {
            _hasEraser = false;
            _isErased = true;
            Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("ParentEraser"));
            eventImage.GetComponent<Image>().sprite = imageArray[12];
        }
    }

    #endregion Table

    #region Clcok
    public RectTransform HourHand;
    public RectTransform MinuteHand;

    public TextMeshProUGUI Hour;
    public TextMeshProUGUI Minute;

    int _minute = 30;
    int _hour = 2;

    bool _dropDoll;

    /*    public void IncreaseMinute()
        {
            _minute = _minute == 55 ? 0 : _minute + 5;

            SetTime();
        }

        public void DecreaseMinute()
        {
            _minute = _minute == 0 ? 55 : _minute - 5;
            SetTime();
        }

        public void IncreaseHour()
        {
            _hour = _hour == 12 ? 1 : _hour + 1;
            SetTime();
        }

        public void DecreaseHour()
        {
            _hour = _hour == 1 ? 12 : _hour - 1;
            SetTime();
        }*/

    public void IncreaseTime(string type)
    {
        if (type == "minute")
        {
            _minute =_minute == 55 ? 0 : _minute + 5;
        }
        else if (type == "hour")
        {
            _hour =_hour == 12 ? 1 : _hour + 1;
        }

        SetTime();
    }

    public void DecreaseTime(string type)
    {
        if (type == "minute")
        {
            _minute = _minute == 0 ? 55 : _minute - 5;
        }
        else if (type == "hour")
        {
            _hour = _hour == 1 ? 12 : _hour - 1;
        }

        SetTime();
    }

    void SetTime()
    {

        MinuteHand.localRotation = Quaternion.Euler(0f, 0f, -_minute * 6);
        HourHand.localRotation = Quaternion.Euler(0f, 0f, -_hour *30);

        Hour.text = string.Format("{0:D2}", _hour);
        Minute.text = string.Format("{0:D2}", _minute);

        if (_minute == 0 && _hour == 12)
        {
            _dropDoll = true;
            CloseEvent();
        }
    }
    #endregion Clock

    #region Bed

    bool _getDoll;
    public void Bed()
    {
        eventImage.SetActive(true);
        backButton.SetActive(true);

        if (!_dropDoll)
        {
            eventImage.GetComponent<Image>().sprite = imageArray[5];
        }
        else
        {
            eventImage.GetComponent<Image>().sprite = imageArray[13];
            eventInteraction.transform.GetChild(5).gameObject.SetActive(true);
        }
        
    }
    public void Doll()
    {
        _getDoll = true;
        _dropDoll = false;
        Inventory.Instance.AddItem(Inventory.Instance.imageList[11]);
        eventImage.GetComponent<Image>().sprite = imageArray[5];
        eventInteraction.transform.GetChild(5).gameObject.SetActive(false);
    }
    #endregion Bed

    #region Door
    public void Door()
    {
        if (_getDoll)
        {
            StartCoroutine(ClearTalk());
        }
    }
    #endregion Door


    #endregion Event
}
