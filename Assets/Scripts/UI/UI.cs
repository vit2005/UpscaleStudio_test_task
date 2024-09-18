using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TimerUI timerUI;

    public void Init()
    {
        timerUI.Init();
    }

    public void GameStart()
    {
        timerUI.StartTimer();
    }
}
