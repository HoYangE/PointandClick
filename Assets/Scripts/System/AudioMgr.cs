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

    public AudioSource seSound;

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

        if (seSound.isPlaying)
        {
            return;
        }
        else
        {
            seSound.Play();
        }
    }

}
