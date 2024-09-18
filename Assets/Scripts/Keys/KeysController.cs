using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private static KeyController _instance;
    public static KeyController instance => _instance;

    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private int keysCount; // кількість ключів

    public Action<int> OnCountChanged;

    private int _keysCount;
    private List<GameObject> keys = new List<GameObject>();


    public void Init()
    {
        _instance = this;
        _keysCount = keysCount;
        OnCountChanged?.Invoke(_keysCount);
    }

    public void SpawnKeys(List<Vector3> freePlaces, Transform parent)
    {
        freePlaces.Shuffle();
        for (int i = 0; i < keysCount; i++)
        {
            var position = new Vector3(freePlaces[i].x, freePlaces[i].y, freePlaces[i].z);
            var rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            RegisterKey(Instantiate(keyPrefab, position, rotation, parent));
        }
        freePlaces.RemoveRange(0, 10);
    }

    public void RegisterKey(GameObject key)
    {
        if (keys.Contains(key)) return;

        keys.Add(key);
        key.GetComponent<KeyItem>().OnKeyFinded += () => UnregisterKey(key);
    }

    public void UnregisterKey(GameObject key)
    {
        if (!keys.Contains(key)) return;

        keys.Remove(key);
        _keysCount--;
        OnCountChanged?.Invoke(_keysCount);
    }

    public GameObject GetNearestKey(Vector3 playerPosition)
    {
        GameObject nearestKey = null;
        float minDistance = Mathf.Infinity;

        foreach (var key in keys)
        {
            float distance = Vector3.Distance(playerPosition, key.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestKey = key;
            }
        }

        return nearestKey;
    }
}
