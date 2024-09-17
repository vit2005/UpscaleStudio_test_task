using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int maxRadius; // Радіус сфери
    public int minRadius; // Радіус центру сфери
    public float noiseScale = 0.1f; // Масштаб для Perlin Noise
    public float threshold = 0.5f; // Поріг для генерації кубів

    private List<Vector3> freePlaces = new List<Vector3>();

    void Start()
    {
        StartCoroutine(GenerateSphereMazeCoroutine());
    }

    IEnumerator GenerateSphereMazeCoroutine()
    {
        int batchSize = 100; // Кількість об'єктів за один крок
        int counter = 0;

        for (int x = -maxRadius; x <= maxRadius; x++)
        {
            for (int y = -maxRadius; y <= maxRadius; y++)
            {
                for (int z = -maxRadius; z <= maxRadius; z++)
                {
                    // Визначаємо, чи точка належить сфері
                    float distance = x * x + y * y + z * z;

                    if (distance <= maxRadius * maxRadius && distance >= minRadius * minRadius)
                    {
                        // Перевіряємо поріг
                        if (CheckPerlinNoize(x, y, z))
                        {
                            // Інстанціюємо префаб
                            Vector3 position = new Vector3(x, y, z);
                            Instantiate(cubePrefab, position, Quaternion.identity);
                            counter++;

                            // Кожного разу, коли досягаємо batchSize, чекаємо наступного кадру
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
        // Обчислюємо Perlin Noise для цієї точки
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

        // Комбінуємо значення шуму
        float combinedPerlinValue = (perlinValue1 + perlinValue2 + perlinValue3) / 3f;

        // Перевіряємо поріг
        return combinedPerlinValue > threshold;
    }

    

}
