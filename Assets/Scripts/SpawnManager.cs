using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", 0, 3);
    }

    void SpawnZombie()
    {
        // create a random spawn point facing the player
        Vector3 spawnPoint = new Vector3(Random.Range(-10, 10), -1, Random.Range(-10, 10));
        
        // create a zombie at the spawn point
        Instantiate(zombiePrefabs, spawnPoint, Quaternion.identity);
    }
}
