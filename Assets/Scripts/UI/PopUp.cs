using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PopUpType
{
    Victory,
    Defeat,
    Pause
}

public class PopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject restartButton;

    private const string VICTORY = "VICTORY";
    private const string DEFEAT = "DEFEAT";
    private const string PAUSE = "PAUSE";

    public void Init(PopUpType type)
    {
        switch (type)
        {
            case PopUpType.Victory:
                title.text = VICTORY;
                break;
            case PopUpType.Defeat:
                title.text = DEFEAT;
                break;
            case PopUpType.Pause:
                title.text = PAUSE;
                break;
            default:
                break;
        }

        resumeButton.SetActive(false);
        restartButton.SetActive(false);
        if (type == PopUpType.Pause)
        {
            resumeButton.SetActive(true);
        }
        else
        {
            restartButton.SetActive(true);
        }
    }
}
