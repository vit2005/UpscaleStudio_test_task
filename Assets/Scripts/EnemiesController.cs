using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private static EnemiesController _instance;
    public static EnemiesController instance => _instance;

    public GameObject enemyPrefab;
    public float enemiesCount = 10; // кількість ворогів

    public void Awake()
    {
        _instance = this;
    }

    public void SpawnEnemies(List<Vector3> freePlaces)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            var position = new Vector3(freePlaces[i].x, freePlaces[i].y, freePlaces[i].z);
            var rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(enemyPrefab, position, rotation);
        }
    }
}
