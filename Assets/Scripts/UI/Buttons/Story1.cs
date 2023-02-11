using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

// 8465 262

public class Story1 : MonoBehaviour
{
    [FormerlySerializedAs("Image")] public Sprite[] imageArray;
    [FormerlySerializedAs("Event")] public GameObject[] eventArray;
    public GameObject image;
    public GameObject jumpScare;
    public GameObject virtualCamera;
    
    public void Start()
    {
        StartCoroutine(Talk());
    }

    public void ClosePopup()
    {
        image.SetActive(false);
        foreach (var eventObject in eventArray)
        {
            if(eventObject != null)
                eventObject.SetActive(false);
        }
    }
    
    #region Start
    IEnumerator Talk()
    {
        var data = CSVReader.Read("Story1TextScript");
        foreach (var t in data)
        {
            switch (int.Parse(t["Talk"].ToString()))
            {
                case 7:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(0));
                    break;
                case 10:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(0));
                    break;
            }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
    }
    #endregion
    
    #region Object
    #region Bookskelf
    private bool _doll = false;
    private bool _hammerItem = false;
    private int _bookshelfLevel = 0;
    public void Bookshelf()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[0 + _bookshelfLevel];
        
        switch (_bookshelfLevel)
        {
            case < 3:
                if (!_hammerItem) break;
                eventArray[16].SetActive(true);
                break;
            case 3:
                eventArray[17].SetActive(true);
                break;
        }
    }
    public void BookshelfAttack()
    {
        if (!_hammerItem) return;
        image.GetComponent<Image>().sprite = imageArray[0 + ++_bookshelfLevel];
        if (_bookshelfLevel != 3) return;
        eventArray[16].SetActive(false);
        eventArray[17].SetActive(true);
        Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("ChildHammer"));
    }
    public void BookshelfDoll()
    {
        image.GetComponent<Image>().sprite = imageArray[0 + ++_bookshelfLevel];
        eventArray[17].SetActive(false);
        _doll = true;
        Inventory.Instance.AddItem(Inventory.Instance.imageList[2]);
    }
    #endregion Bookskelf
    public void Book()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[5];
    }
    #region Table
    private int _tableLevel = 0;
    public void Table()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[6 + _tableLevel];
        if(_keyItem)
            eventArray[_tableLevel].SetActive(true);
    }
    public void Table2Level()
    {
        Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("ChildKey"));
        eventArray[0].SetActive(false);
        image.GetComponent<Image>().sprite = imageArray[6 + ++_tableLevel];
        eventArray[1].SetActive(true);
    }
    public void Table3Level()
    {
        eventArray[1].SetActive(false);
        _hammerItem = true;
        Inventory.Instance.AddItem(Inventory.Instance.imageList[1]);
        image.GetComponent<Image>().sprite = imageArray[6 + ++_tableLevel];
        
    }
    #endregion Table
    public void Bed()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[11];
    }
    public void AnimalPoster()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[10];
    }
    #region DollAndRose
    private bool _jumpScare = false;
    private static readonly int Open = Animator.StringToHash("open");
    public void DollAndRose()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[9];
        if(_jumpScare) return;
        eventArray[18].SetActive(true);
    }
    public void DollAndRose2()
    {
        _jumpScare = true;
        eventArray[18].SetActive(false);
        jumpScare.SetActive(true);
        OpenJumpScare();
    }
    private void OpenJumpScare()
    {
        if (jumpScare == null) return;
        var animator = jumpScare.transform.GetChild(2).GetComponent<Animator>();
        if (animator == null) return;
        animator.SetTrigger(Open);
        StartCoroutine(JumpScare());
        virtualCamera.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1.0f;
    }
    private IEnumerator JumpScare()
    {
        yield return new WaitForSeconds(1.0f);
        jumpScare.SetActive(false);
        virtualCamera.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        yield return null;
    }
    #endregion DollAndRose
    #region LovePoster
    private int _lovePosterLevel = 0;
    public void LovePoster()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[12 + _lovePosterLevel];
        switch (_lovePosterLevel)
        {
            case 0:
                eventArray[2].SetActive(true);
                eventArray[3].SetActive(true);
                break;
            case 1:
                eventArray[2].SetActive(true);
                break;
            case 2:
                eventArray[3].SetActive(true);
                break;
            case 3:
                eventArray[4].SetActive(true);
                break;
        }
    }
    public void LovePosterL()
    {
        image.SetActive(true);
        if(_lovePosterLevel == 0)
            image.GetComponent<Image>().sprite = imageArray[14];
        else
        {
            image.GetComponent<Image>().sprite = imageArray[15];
            eventArray[4].SetActive(true);
        }
        _lovePosterLevel+=2;
        eventArray[2].SetActive(false);
    }
    public void LovePosterR()
    {
        image.SetActive(true);
        if(_lovePosterLevel == 0)
            image.GetComponent<Image>().sprite = imageArray[13];
        else
        {
            image.GetComponent<Image>().sprite = imageArray[15];
            eventArray[4].SetActive(true);
        }
        _lovePosterLevel++;
        eventArray[3].SetActive(false);
    }
    public void LovePoster2Level()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[12 + ++_lovePosterLevel];
        eventArray[4].SetActive(false);
    }
    #endregion LovePoster
    #region Drawer
    private int _drawerLevel = 0;
    private bool _keyItem = false;
    private bool _cardItem = false;
    public void Drawer()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[17 + _drawerLevel];
        Debug.Log(_drawerLevel);
        switch (_drawerLevel)
        {
            case 0:
                eventArray[5].SetActive(true);
                eventArray[6].SetActive(true);
                break;
            case 1:
                eventArray[5].SetActive(true);
                break;
            case 2:
                eventArray[6].SetActive(true);
                if (_cardItem)
                {
                    _drawerLevel = 5;
                    image.GetComponent<Image>().sprite = imageArray[17 + _drawerLevel];
                }
                break;
            case 3:
                eventArray[15].SetActive(true);
                break;
            case 5:
                eventArray[14].SetActive(true);
                break;
            case 6:
                eventArray[15].SetActive(true);
                eventArray[5].SetActive(true);
                break;
        }
    }
    public void DrawerLockAnimal()
    {
        eventArray[5].SetActive(false);
        eventArray[6].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[24];
        eventArray[7].SetActive(true);
        eventArray[8].SetActive(true);
        eventArray[9].SetActive(true);
    }
    public void DrawerLockHeart()
    {
        eventArray[5].SetActive(false);
        eventArray[6].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[25];
        eventArray[10].SetActive(true);
        eventArray[11].SetActive(true);
        eventArray[12].SetActive(true);
        eventArray[13].SetActive(true);
    }
    private string _currentAnimalPassword = "000";
    public void DrawerLockAnimal1()
    {
        var component = eventArray[7].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentAnimalPassword = _currentAnimalPassword[..0] + component.text + _currentAnimalPassword.Substring(1, 2);
        DrawerAnimalPassword(_currentAnimalPassword);
    }
    public void DrawerLockAnimal2()
    {
        var component = eventArray[8].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentAnimalPassword = _currentAnimalPassword[..1] + component.text + _currentAnimalPassword.Substring(2, 1);
        DrawerAnimalPassword(_currentAnimalPassword);
    }
    public void DrawerLockAnimal3()
    {
        var component = eventArray[9].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentAnimalPassword = _currentAnimalPassword[..2] + component.text;
        DrawerAnimalPassword(_currentAnimalPassword);
    }
    private string _currentHeartPassword = "0000";
    public void DrawerLockHeart1()
    {
        var component = eventArray[10].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentHeartPassword = _currentHeartPassword[..0] + component.text + _currentHeartPassword.Substring(1, 3);
        DrawerAnimalPassword(_currentHeartPassword);
    }
    public void DrawerLockHeart2()
    {
        var component = eventArray[11].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentHeartPassword = _currentHeartPassword[..1] + component.text + _currentHeartPassword.Substring(2, 2);
        DrawerHeartPassword(_currentHeartPassword);
    }
    public void DrawerLockHeart3()
    {
        var component = eventArray[12].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentHeartPassword = _currentHeartPassword[..2] + component.text + _currentHeartPassword.Substring(3, 1);
        DrawerHeartPassword(_currentHeartPassword);
    }
    public void DrawerLockHeart4()
    {
        var component = eventArray[13].transform.GetChild(0).GetComponent<TMP_Text>();
        component.text = TenToZero(component.text);
        _currentHeartPassword = _currentHeartPassword[..3] + component.text;
        DrawerHeartPassword(_currentHeartPassword);
    }
    public void DrawerAnimalItem()
    {
        _keyItem = true;
        Inventory.Instance.AddItem(Inventory.Instance.imageList[0]);
        eventArray[14].SetActive(false);
        image.SetActive(true);
        if (!_cardItem)
        {
            eventArray[6].SetActive(true);
            _drawerLevel = 2;
        }
        else
            _drawerLevel = 3;
        image.GetComponent<Image>().sprite = imageArray[17 + _drawerLevel];
    }
    public void DrawerHeartItem()
    {
        _cardItem = true;
        eventArray[15].SetActive(false);
        image.SetActive(true);
        _drawerLevel = !_keyItem ? 6 : 3;
        image.GetComponent<Image>().sprite = imageArray[26];
    }
    private string TenToZero(string str)
    {
        var temp = int.Parse(str) + 1;
        if (temp == 10)
            temp = 0;
        return temp.ToString();
    }
    private void DrawerAnimalPassword(string str)
    {
        if (str != "262") return;
        eventArray[7].SetActive(false);
        eventArray[8].SetActive(false);
        eventArray[9].SetActive(false);
        image.SetActive(true);
        if (_drawerLevel == 0)
        {
            eventArray[14].SetActive(true);
            _drawerLevel = 5;
            if(_keyItem) return;
            image.GetComponent<Image>().sprite = imageArray[22];
        }
        else if(_drawerLevel != 0)
        {
            eventArray[14].SetActive(true);
            _drawerLevel = 5;
            if(_keyItem) return;
            image.GetComponent<Image>().sprite = imageArray[22];
        }
    }
    private void DrawerHeartPassword(string str)
    {
        if (str != "8465") return;
        eventArray[10].SetActive(false);
        eventArray[11].SetActive(false);
        eventArray[12].SetActive(false);
        eventArray[13].SetActive(false);
        image.SetActive(true);
        if (_drawerLevel == 0)
        {
            eventArray[15].SetActive(true);
            image.GetComponent<Image>().sprite = imageArray[23];
            _drawerLevel = 6;
        }
        else if(_drawerLevel != 0)
        {
            eventArray[15].SetActive(true);
            image.GetComponent<Image>().sprite = imageArray[23];
            _drawerLevel = 6;
        }
    }
    #endregion Drawer
    #endregion Object

    #region Door
    public void Door()
    {
        if (!_doll) return;
        Inventory.Instance.AllRemoveItem();
        GameMgr.Instance.ChangeScene("Scenes/Story2Scene");
    }
    #endregion Door
}
