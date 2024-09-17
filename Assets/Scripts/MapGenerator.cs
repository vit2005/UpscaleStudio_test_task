using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int maxRadius; // ����� �����
    public int minRadius; // ����� ������ �����
    public float noiseScale = 0.1f; // ������� ��� Perlin Noise
    public float threshold = 0.5f; // ���� ��� ��������� ����

    private List<Vector3> freePlaces = new List<Vector3>();

    void Start()
    {
        StartCoroutine(GenerateSphereMazeCoroutine());
    }

    IEnumerator GenerateSphereMazeCoroutine()
    {
        int batchSize = 100; // ʳ������ ��'���� �� ���� ����
        int counter = 0;

        for (int x = -maxRadius; x <= maxRadius; x++)
        {
            for (int y = -maxRadius; y <= maxRadius; y++)
            {
                for (int z = -maxRadius; z <= maxRadius; z++)
                {
                    // ���������, �� ����� �������� ����
                    float distance = x * x + y * y + z * z;

                    if (distance <= maxRadius * maxRadius && distance >= minRadius * minRadius)
                    {
                        // ���������� ����
                        if (CheckPerlinNoize(x, y, z))
                        {
                            // ������������ ������
                            Vector3 position = new Vector3(x, y, z);
                            Instantiate(cubePrefab, position, Quaternion.identity);
                            counter++;

                            // ������� ����, ���� �������� batchSize, ������ ���������� �����
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

        KeyController.instance.SpawnKeys(freePlaces);
    }

    private bool CheckPerlinNoize(int x, int y, int z)
    {
        // ���������� Perlin Noise ��� ���� �����
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

        // �������� �������� ����
        float combinedPerlinValue = (perlinValue1 + perlinValue2 + perlinValue3) / 3f;

        // ���������� ����
        return combinedPerlinValue > threshold;
    }

    

}
