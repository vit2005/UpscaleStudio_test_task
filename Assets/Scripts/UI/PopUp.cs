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

    private Dictionary<PopUpType, string> _titles = new Dictionary<PopUpType, string>();

    private void Awake()
    {
        _titles.Add(PopUpType.Victory, VICTORY);
        _titles.Add(PopUpType.Defeat, DEFEAT);
        _titles.Add(PopUpType.Pause, PAUSE);
        gameObject.SetActive(false);
    }

    public void Init(PopUpType type)
    {
        title.text = _titles[type];

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
