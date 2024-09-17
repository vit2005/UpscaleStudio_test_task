using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int maxRadius; // ����� �����
    public int minRadius; // ����� ������ �����
    public float noiseScale = 0.1f; // ������� ��� Perlin Noise
    public float threshold = 0.5f; // ���� ��� ��������� ����

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
                        if (combinedPerlinValue > threshold)
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
                    }
                }
            }
        }
    }
}