using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController instance => _instance;

    [SerializeField] private Transform parent;
    [SerializeField] private FirstPersonCameraRotation rotation;
    [SerializeField] private MovementController movement;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private KeyController keysController;
    [SerializeField] private EnemiesController enemiesController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private Compas compas;
    [SerializeField] private FinalPortal finalPortal;
    [SerializeField] private UI ui;
    [SerializeField] private SimpleRotation planetRotation;
    [SerializeField] private AudioSource backgroundMusic;

    private bool _paused = false;
    public bool Paused => _paused;

    public void Awake()
    {
        _instance = this;
        ui.Init();

        healthController.OnHealthChanged += ui.SetHealth;
        healthController.OnHealthChanged += (float value) => { if (Mathf.Approximately(value, 0f)) Defeat(); };
        healthController.Init();

        keysController.OnCountChanged += ui.SetCountText;
        keysController.OnCountChanged += (int value) => { if (value == 0) ShowFinalPortal(); };
        keysController.Init();

        mapGenerator.OnGenerationFinished += OnMapGenerated;
        mapGenerator.GenerateMap(parent);

        movement.Init();

        SetRotationAndMovement(false);
    }

    private void ShowFinalPortal()
    {
        finalPortal.gameObject.SetActive(true);
        compas.SetTarget(finalPortal.gameObject);
        finalPortal.OnFinalPortal += Victory;
    }

    private void OnMapGenerated()
    {
        keysController.SpawnKeys(mapGenerator.freePlaces, parent);
        enemiesController.SpawnEnemies(mapGenerator.freePlaces, parent);
        ui.GameStart();
        SetRotationAndMovement(true);
        planetRotation.enabled = true;
    }

    public void SetRotationAndMovement(bool value)
    {
        _paused = !value;
        movement.Pause(value);
        movement.enabled = value;
    }

    public void SetBackgroundMusic(PopUpType type)
    {
        if (type == PopUpType.Pause) {
            if (backgroundMusic.isPlaying) backgroundMusic.Pause();
            else backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ui.ShowPopup(PopUpType.Pause);
    }

    private void Victory()
    {
        ui.ShowPopup(PopUpType.Victory);
    }

    private void Defeat()
    {
        ui.ShowPopup(PopUpType.Defeat);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    internal void Exit()
    {
        Application.Quit();
    }
}
