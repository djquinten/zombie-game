using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
