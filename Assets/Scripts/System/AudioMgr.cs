using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioMgr : MonoBehaviour
{
    private static AudioMgr _instance = null;

    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    public TextMeshProUGUI masterVolume;
    public TextMeshProUGUI bgmVolume;
    public TextMeshProUGUI seVolume;

    public GameObject option;

    public AudioSource seSound;
    public AudioClip[] seClip;

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

    public static AudioMgr Instance => _instance == null ? null : _instance;

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
        float _masterVolume = Mathf.Floor(masterSlider.value * 100);
        masterVolume.text = _masterVolume.ToString();
    }

    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        float _bgmVolume = Mathf.Floor(bgmSlider.value * 100);
        bgmVolume.text = _bgmVolume.ToString();
    }

    public void SetSeVolume()
    {
        audioMixer.SetFloat("SE", Mathf.Log10(seSlider.value) * 20);
        float _seVolume = Mathf.Floor(seSlider.value * 100);
        seVolume.text = _seVolume.ToString();
        seSound.clip = seClip[0];
        
        if (seSound.isPlaying) return;
        seSound.Play();
    }

    #region Clip
    public void ButtonClick()
    {
        seSound.clip = seClip[0];
        seSound.Play();
    }
    
    public void Clue()
    {
        seSound.clip = seClip[1];
        seSound.Play();
    }
    
    public void Door_open()
    {
        seSound.clip = seClip[2];
        seSound.Play();
    }
    
    public void Glass_crack()
    {
        seSound.clip = seClip[3];
        seSound.Play();
    }

    public void Hammer()
    {
        seSound.clip = seClip[4];
        seSound.Play();
    }
    
    public void Lock()
    {
        seSound.clip = seClip[5];
        seSound.Play();
    }
    
    public void Pickup()
    {
        seSound.clip = seClip[6];
        seSound.Play();
    }
    
    public void Put_object()
    {
        seSound.clip = seClip[7];
        seSound.Play();
    }
    
    public void Suddenly()
    {
        seSound.clip = seClip[8];
        seSound.Play();
    }
    
    public void Unlock()
    {
        seSound.clip = seClip[9];
        seSound.Play();
    }
    #endregion
    
    
    public void OptionOn()
    {
        option.SetActive(true);
    }
    
    public void OptionOff()
    {
        option.SetActive(false);
    }
}
