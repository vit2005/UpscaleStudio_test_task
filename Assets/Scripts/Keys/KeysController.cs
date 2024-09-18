using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private static KeyController _instance;
    public static KeyController instance => _instance;

    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private float keysCount = 10; // ������� ������
    [SerializeField] private TextMeshProUGUI countText;

    private List<GameObject> keys = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
        countText.text = keysCount.ToString();
    }

    public void SpawnKeys(List<Vector3> freePlaces)
    {
        freePlaces.Shuffle();
        for (int i = 0; i < keysCount; i++)
        {
            var position = new Vector3(freePlaces[i].x, freePlaces[i].y, freePlaces[i].z);
            var rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            RegisterKey(Instantiate(keyPrefab, position, rotation));
        }
        freePlaces.RemoveRange(0, 10);
        EnemiesController.instance.SpawnEnemies(freePlaces);
    }

    public void RegisterKey(GameObject key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
        }
    }

    public void UnregisterKey(GameObject key)
    {
        if (keys.Contains(key))
        {
            keys.Remove(key);
        }
        keysCount--;
        countText.text = keysCount.ToString();
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