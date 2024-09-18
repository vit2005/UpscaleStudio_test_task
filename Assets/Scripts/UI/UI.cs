using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class UI : MonoBehaviour
{
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private PopUp popUp;
    [SerializeField] private HealthBar healthBar;
    [Space]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openPopUpSound;
    [SerializeField] private AudioClip closePopUpSound;
    [SerializeField] private AudioClip glitchSound;

    private PopUpType? _popUpType = null;

    public void Init()
    {
        timerUI.Init();
    }

    public void GameStart()
    {
        timerUI.StartTimer();
    }

    public void SetCountText(int value)
    {
        countText.text = value.ToString();
    }

    public void SetHealth(float value)
    {
        healthBar.SetPercentage(value);
    }

    public void PauseToggle()
    {
        timerUI.PauseToggle();
    }

    public void ShowPopup(PopUpType type)
    {
        if (_popUpType == type)
        {
            if (_popUpType == PopUpType.Pause)
                ClosePopUp();
            return;
        }
        else if (_popUpType.HasValue)
        {
            if (_popUpType.Value == PopUpType.Pause && type != PopUpType.Pause)
            {
                ClosePopUp();
            }
            audioSource.PlayOneShot(glitchSound);
            return;
        }

        _popUpType = type;

        audioSource.PlayOneShot(openPopUpSound);

        popUp.Init(type);
        popUp.gameObject.SetActive(true);

        PauseToggle();

        GameController.instance.SetRotationAndMovement(false);
        GameController.instance.SetBackgroundMusic(type);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePopUp()
    {
        if (_popUpType != PopUpType.Pause)
        {
            audioSource.PlayOneShot(glitchSound);
            return;
        }

        audioSource.PlayOneShot(closePopUpSound);

        popUp.gameObject.SetActive(false);
        _popUpType = null;

        PauseToggle();

        GameController.instance.SetRotationAndMovement(true);
        GameController.instance.SetBackgroundMusic(PopUpType.Pause);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartClick()
    {
        GameController.instance.Restart();
    }

    public void ExitClick()
    {
        GameController.instance.Exit();
    }
}
