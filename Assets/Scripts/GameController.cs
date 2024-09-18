using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController instance => _instance;

    [SerializeField] private Transform parent;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private KeyController keysController;
    [SerializeField] private EnemiesController enemiesController;
    [SerializeField] private UI ui;

    public void Awake()
    {
        _instance = this;
        ui.Init();
        mapGenerator.OnGenerationFinished += OnMapGenerated;
        mapGenerator.GenerateMap(parent);
    }

    private void OnMapGenerated()
    {
        keysController.SpawnKeys(mapGenerator.freePlaces, parent);
        enemiesController.SpawnEnemies(mapGenerator.freePlaces, parent);
        ui.GameStart();
    }
}
