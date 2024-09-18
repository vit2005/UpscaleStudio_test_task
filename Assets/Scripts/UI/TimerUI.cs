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
        text.text = "00:000";
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
            // Збільшуємо пройдений час
            _time += Time.deltaTime;

            // Конвертуємо пройдений час у TimeSpan
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);

            // Відображаємо час у форматі SS:MMM
            text.text = string.Format("{0:00}:{1:000}",
                                            timeSpan.Seconds,
                                            timeSpan.Milliseconds);
        }
    }
}
