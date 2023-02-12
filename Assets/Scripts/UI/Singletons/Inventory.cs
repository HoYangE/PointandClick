using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance = null;

    public List<GameObject> inventoryButton = new List<GameObject>();
    public List<Sprite> inventoryImage = new List<Sprite>();
    public List<Sprite> imageList = new List<Sprite>();
    
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

    public static Inventory Instance => _instance == null ? null : _instance;

    public void Menu()
    {
        GameMgr.Instance.ChangeScene("Scenes/TitleScene");
    }
    
    public void AddItem(Sprite item)
    {
        if(inventoryImage.Count > 5) return;
        inventoryImage.Add(item);
        var component = inventoryButton[inventoryImage.Count-1].GetComponent<Image>();
        component.color = Color.white;
        component.sprite = item;
    }
    public void RemoveItem(int index)
    {
        if(index > inventoryImage.Count) return;
        if(NoRemove(inventoryImage[index].name)) return;
        inventoryImage.RemoveAt(index);
        for(var i = index;i<inventoryImage.Count;i++)
        {
            var image = inventoryButton[i].GetComponent<Image>();
            image.color = Color.white;
            image.sprite = inventoryImage[i];
        }
        var component = inventoryButton[inventoryImage.Count].GetComponent<Image>();
        component.color = Color.clear;
        component.sprite = null;
    }
    public int FindItem(string str)
    {
        for(var i = 0;i<inventoryImage.Count;i++)
        {
            if(inventoryImage[i].name == str)
            {
                return i;
            }
        }
        return -1;
    }
    public void AllRemoveItem()
    {
        var count = 0;
        var loopSize = inventoryImage.Count;
        for(var i = 0;i<loopSize;i++)
        {
            if (NoRemove(inventoryImage[count].name))
            {
                count++;
                continue;
            }
            RemoveItem(count);
        }
    }

    private bool NoRemove(string str)
    {
        if (str == "ChildDoll1" || str == "PassageDoll1")
            return true;
        return false;
    }
}
