using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Story3 : MonoBehaviour
{
    [FormerlySerializedAs("Image")] public Sprite[] imageArray;
    [FormerlySerializedAs("Event")] public GameObject[] eventArray;
    public GameObject[] ladder;
    public GameObject background;
    public GameObject image;
    public GameObject doll;
    public GameObject jumpScare;
    public GameObject virtualCamera;
    
    public void Start()
    {
        StartCoroutine(Talk());
        if (Inventory.Instance.FindItem("ChildDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[2]);
        if (Inventory.Instance.FindItem("ParentDoll1") == -1)
            Inventory.Instance.AddItem(Inventory.Instance.imageList[11]);
        PlayerPrefs.SetInt("Level", 3);
    }

    public void ClosePopup()
    {
        image.SetActive(false);
        foreach (var eventObject in eventArray)
        {
            if(eventObject != null)
                eventObject.SetActive(false);
        }

        if (!_ladderItem) return;
        ladder[0].SetActive(true);
        ladder[1].SetActive(true);
    }
    
    #region Start
    IEnumerator Talk()
    {
        var data = CSVReader.Read("Story3TextScript");
        foreach (var t in data)
        {
            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
    }
    IEnumerator Talk2()
    {
        var data = CSVReader.Read("Story3EndTextScript");
        foreach (var t in data)
        {
            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
        Inventory.Instance.AllRemoveItem();
        GameMgr.Instance.ChangeScene("Scenes/Story4Scene");
    }
    private IEnumerator FadeOutCoroutine()
    {
        var fadeCount = 0.0f;
        var dollImage = doll.GetComponent<Image>();
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
            dollImage.color = new Color(1, 1, 1, fadeCount);
        }
    }
    #endregion
    
    #region Object
    #region Box
    private int _boxLevel = 0;
    private bool _ladderItem = false;
    public void Box()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[0 + _boxLevel];
        switch (_boxLevel)
        {
            case 0:
                eventArray[6].SetActive(true);
                break;
            case 1:
                if(!_knifeItem) return;
                eventArray[7].SetActive(true);
                break;
            case 2:
                eventArray[8].SetActive(true);
                break;
        }
    }
    public void Box2()
    {
        eventArray[6].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[0 + ++_boxLevel];
        if(!_knifeItem) return;
        eventArray[7].SetActive(true);
    }
    public void Box3()
    {
        eventArray[7].SetActive(false);
        image.SetActive(true);
        Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("PassageKnife"));
        image.GetComponent<Image>().sprite = imageArray[0 + ++_boxLevel];
        eventArray[8].SetActive(true);
    }
    public void Box4()
    {
        eventArray[8].SetActive(false);
        image.SetActive(true);
        _ladderItem = true;
        AudioMgr.Instance.Pickup();
        _ladderInventory = true;
        background.GetComponent<Image>().sprite = imageArray[14];
        Inventory.Instance.AddItem(Inventory.Instance.imageList[6]);
        image.GetComponent<Image>().sprite = imageArray[0 + ++_boxLevel];
    }
    #endregion Box
    #region Radio
    private int _radioLevel = 0;
    private bool _hammerItem = false;
    public void Radio()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[11 + _radioLevel];
        switch (_radioLevel)
        {
            case 0:
                eventArray[2].SetActive(true);
                break;
            case 1:
                eventArray[3].SetActive(true);
                break;
        }
    }
    public void Radio2()
    {
        eventArray[2].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[11 + ++_radioLevel];
        eventArray[3].SetActive(true);
    }
    public void Radio3()
    {
        eventArray[3].SetActive(false);
        image.SetActive(true);
        _hammerItem = true;
        AudioMgr.Instance.Pickup();
        Inventory.Instance.AddItem(Inventory.Instance.imageList[4]);
        image.GetComponent<Image>().sprite = imageArray[11 + ++_radioLevel];
    }
    #endregion Radio
    #region FlowerPotDrawer
    private int _flowerPotDrawerLevel = 0;
    private bool _knifeItem = false;
    public void FlowerPotDrawer()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[8 + _flowerPotDrawerLevel];
        if(!_hammerItem) return;
        switch (_flowerPotDrawerLevel)
        {
            case 0:
                eventArray[4].SetActive(true);
                break;
            case 1:
                eventArray[5].SetActive(true);
                break;
        }
    }
    public void FlowerPotDrawer2()
    {
        AudioMgr.Instance.Glass_crack();
        eventArray[4].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[8 + ++_flowerPotDrawerLevel];
        Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("PassageHammer"));
        eventArray[5].SetActive(true);
    }
    public void FlowerPotDrawer3()
    {
        eventArray[5].SetActive(false);
        image.SetActive(true);
        _knifeItem = true;
        AudioMgr.Instance.Pickup();
        Inventory.Instance.AddItem(Inventory.Instance.imageList[5]);
        image.GetComponent<Image>().sprite = imageArray[8 + ++_flowerPotDrawerLevel];
    }
    #endregion FlowerPotDrawer
    #region CardDrawer
    private int _cardDrawerLevel = 0;
    public void CardDrawer()
    {
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[5 + _cardDrawerLevel];
        switch (_cardDrawerLevel)
        {
            case 0:
                eventArray[0].SetActive(true);
                break;
            case 1:
                eventArray[1].SetActive(true);
                break;
        }
    }
    public void CardDrawer2()
    {
        eventArray[0].SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = imageArray[5 + ++_cardDrawerLevel];
        eventArray[1].SetActive(true);
    }
    public void CardDrawer3()
    {
        AudioMgr.Instance.Clue();
        eventArray[1].SetActive(false);
        image.GetComponent<Image>().sprite = imageArray[4];
        image.SetActive(true);
    }
    #endregion CardDrawer
    #region Ladder
    private bool _ladderInventory = false;
    private bool _paintingItem = false;
    private bool _mirrorItem = false;
    public void Painting()
    {
        Inventory.Instance.AddItem(Inventory.Instance.imageList[3]);
        _paintingItem = true;
        ladder[2].SetActive(false);
        if(_mirrorItem)
            ladder[4].SetActive(true);
        if(_mirrorItem)
            background.GetComponent<Image>().sprite = imageArray[19];
        else
            background.GetComponent<Image>().sprite = imageArray[22];
    }
    public void Mirror()
    {
        ladder[3].SetActive(false);
        Inventory.Instance.AddItem(Inventory.Instance.imageList[7]);
        _mirrorItem = true;
        if(_paintingItem)
            background.GetComponent<Image>().sprite = imageArray[25];
        else
            background.GetComponent<Image>().sprite = imageArray[16];
    }
    public void MirrorMove()
    {
        ladder[4].SetActive(false);
        ladder[0].SetActive(false);
        background.GetComponent<Image>().sprite = imageArray[20];
        Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("PassageMirror"));
        doll.SetActive(true);
        StartCoroutine(FadeOutCoroutine());
    }
    public void PaintingLadder()
    {
        if (_ladderInventory)
        {
            Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("PassageLadder"));
            ladder[1].SetActive(false);
            ladder[0].SetActive(true);
            _ladderInventory = false;
            AudioMgr.Instance.Put_object();
            if(!_paintingItem)
                ladder[2].SetActive(true);
            else if(_mirrorItem)
                ladder[4].SetActive(true);
            background.GetComponent<Image>().sprite = _paintingItem switch
            {
                true when _mirrorItem => imageArray[19],
                true when !_mirrorItem => imageArray[22],
                false when _mirrorItem => imageArray[18],
                false when !_mirrorItem => imageArray[21],
                _ => background.GetComponent<Image>().sprite
            };
        }
        else
        {
            Inventory.Instance.AddItem(Inventory.Instance.imageList[6]);
            ladder[1].SetActive(true);
            ladder[0].SetActive(true);
            ladder[2].SetActive(false);
            AudioMgr.Instance.Pickup();
            _ladderInventory = true;
            background.GetComponent<Image>().sprite = _paintingItem switch
            {
                true when _mirrorItem => imageArray[27],
                true when !_mirrorItem => imageArray[23],
                false when _mirrorItem => imageArray[17],
                false when !_mirrorItem => imageArray[14],
                _ => background.GetComponent<Image>().sprite
            };
        }
    }
    public void MirrorLadder()
    {
        if (_ladderInventory)
        {
            Inventory.Instance.RemoveItem(Inventory.Instance.FindItem("PassageLadder"));
            ladder[0].SetActive(false);
            ladder[1].SetActive(true);
            AudioMgr.Instance.Put_object();
            if(!_mirrorItem)
                ladder[3].SetActive(true);
            _ladderInventory = false;
            background.GetComponent<Image>().sprite = _paintingItem switch
            {
                true when _mirrorItem => imageArray[25],
                true when !_mirrorItem => imageArray[24],
                false when _mirrorItem => imageArray[16],
                false when !_mirrorItem => imageArray[15],
                _ => background.GetComponent<Image>().sprite
            };
        }
        else
        {
            Inventory.Instance.AddItem(Inventory.Instance.imageList[6]);
            ladder[0].SetActive(true);
            ladder[1].SetActive(true);
            ladder[3].SetActive(false);
            AudioMgr.Instance.Pickup();
            _ladderInventory = true;
            background.GetComponent<Image>().sprite = _paintingItem switch
            {
                true when _mirrorItem => imageArray[27],
                true when !_mirrorItem => imageArray[23],
                false when _mirrorItem => imageArray[17],
                false when !_mirrorItem => imageArray[14],
                _ => background.GetComponent<Image>().sprite
            };
        }
    }
    #endregion Ladder
    #region Doll
    private bool _doll = false;
    private static readonly int Open = Animator.StringToHash("open");
    public void Doll()
    {
        Inventory.Instance.AddItem(Inventory.Instance.imageList[8]);
        doll.SetActive(false);
        jumpScare.SetActive(true);
        OpenJumpScare();
        _doll = true;
        AudioMgr.Instance.Suddenly();
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
        yield return new WaitForSeconds(1.35f);
        jumpScare.SetActive(false);
        virtualCamera.GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        yield return null;
    }
    #endregion Doll
    #endregion Object

    #region Door
    public void Door()
    {
        if (!_doll)
        {
            AudioMgr.Instance.Lock();
            return;
        }
        AudioMgr.Instance.Door_open();
        StartCoroutine(Talk2());
    }
    #endregion Door
}
