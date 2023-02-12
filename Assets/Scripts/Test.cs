using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public RectTransform Hour;
    public RectTransform Minute;

    float _minute = 0;
    float _hour = 2;

    void Start()
    {
        SetTime();
    }

    void Update()
    {
        
    }

    public void IncreaseMinute()
    {
        _minute = _minute >= 60 ? 0 : _minute + 5;

        SetTime();
    }

    public void DecreaseMinute()
    {
        _minute = _minute <= 0 ? 55 : _minute - 5;
        SetTime();

    }

    public void IncreaseHour()
    {
        _hour = _hour >= 12 ? 1 : _hour + 1;
        SetTime();
    }

    public void DecreaseHour()
    {
        _hour = _hour <= 0 ? 11 : _hour - 1;
        SetTime();
    }

    void SetTime()
    {
        _minute = (_minute / 60f) * 360f;
        _hour = (_hour / 12f) * 360f;

        Minute.localRotation = Quaternion.Euler(0f, 0f, -_minute);
        Hour.localRotation = Quaternion.Euler(0f, 0f, -_hour);

        _minute = (_minute / 360f) * 60f;
        _hour = (_hour / 360f) * 12f;
    }
}
