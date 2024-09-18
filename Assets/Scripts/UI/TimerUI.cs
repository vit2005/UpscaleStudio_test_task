using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private float _time;
    private bool _isPlaying = false;

    public void Init()
    {
        text.text = "0:00:000";
    }

    public void StartTimer()
    {
        _time = 0f;
        _isPlaying = true;
    }

    public void PauseToggle()
    {
        _isPlaying = !_isPlaying;
    }

    public void Update()
    {
        if (_isPlaying)
        {
            _time += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
            text.text = string.Format("{0:0}:{1:00}:{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}
