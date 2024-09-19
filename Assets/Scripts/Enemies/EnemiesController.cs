using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemiesCount;

    public void SpawnEnemies(List<Vector3> freePlaces, Transform parent)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            var position = new Vector3(freePlaces[i].x, freePlaces[i].y, freePlaces[i].z);
            var rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(enemyPrefab, position, rotation, parent);
        }
    }
}
