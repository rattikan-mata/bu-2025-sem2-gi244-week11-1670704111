using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class WaveSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public List<Wave> waves = new List<Wave>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        int waveCount = 1;

        foreach (Wave wave in waves)
        {
            Debug.Log("Start Wave " + waveCount);

            List<Transform> selectedPoints = new List<Transform>();
            List<Transform> temp = new List<Transform>(spawnPoints);

            for (int i = 0; i < wave.numberOfRandomSpawnPoint; i++)
            {
                int index = Random.Range(0, temp.Count);
                selectedPoints.Add(temp[index]);
                temp.RemoveAt(index);
            }

            for (int i = 0; i < wave.numberOfPowerUp; i++)
            {
                Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Vector3 spawnPos = point.position + new Vector3(0, 1.5f, 0);
                Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(wave.delayStart);

            for (int i = 0; i < wave.totalSpawnEnemies; i++)
            {
                Transform point = selectedPoints[Random.Range(0, selectedPoints.Count)];
                Instantiate(enemyPrefab, point.position, Quaternion.identity);

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            Debug.Log("End Wave " + waveCount);
            waveCount++;
        }
    }
}
