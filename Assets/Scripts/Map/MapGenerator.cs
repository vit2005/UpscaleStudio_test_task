using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int maxRadius;
    [SerializeField] private int minRadius;
    [SerializeField] private float noiseScale = 0.1f;
    [SerializeField] private float threshold = 0.5f;

    public List<Vector3> freePlaces = new List<Vector3>();
    public Action OnGenerationFinished;

    public void GenerateMap(Transform parent)
    {
        StartCoroutine(GenerateSphereMazeCoroutine(parent));
    }

    private IEnumerator GenerateSphereMazeCoroutine(Transform parent)
    {
        int batchSize = 100;
        int counter = 0;

        for (int x = -maxRadius; x <= maxRadius; x++)
        {
            for (int y = -maxRadius; y <= maxRadius; y++)
            {
                for (int z = -maxRadius; z <= maxRadius; z++)
                {
                    float distance = x * x + y * y + z * z;

                    if (distance <= maxRadius * maxRadius && distance >= minRadius * minRadius)
                    {
                        if (CheckPerlinNoize(x, y, z))
                        {
                            Vector3 position = new Vector3(x, y, z);
                            Instantiate(cubePrefab, position, Quaternion.identity, parent);
                            counter++;

                            if (counter >= batchSize)
                            {
                                counter = 0;
                                yield return null;
                            }
                        }
                        else
                        {
                            freePlaces.Add(new Vector3(x, y, z));
                        }
                    }
                }
            }
        }
        OnGenerationFinished?.Invoke();
    }

    private bool CheckPerlinNoize(int x, int y, int z)
    {
        float perlinValue1 = Mathf.PerlinNoise(
            (x + maxRadius) * noiseScale,
            (y + maxRadius) * noiseScale
        );
        float perlinValue2 = Mathf.PerlinNoise(
            (y + maxRadius) * noiseScale,
            (z + maxRadius) * noiseScale
        );
        float perlinValue3 = Mathf.PerlinNoise(
            (z + maxRadius) * noiseScale,
            (x + maxRadius) * noiseScale
        );

        float combinedPerlinValue = (perlinValue1 + perlinValue2 + perlinValue3) / 3f;

        return combinedPerlinValue > threshold;
    }
}
