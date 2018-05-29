using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour {

    public GameObject enemyObject;
    public Transform[] spawns;

    //[Range(0.0f, 1.0f)]
    //public float spawnRate = 0.5f; // Probability an enemy will spawn
    //public float spawnIncrease = 0f;
    //public int countToIncreaseSpawnRate = 0; // Sets number of times for function to repeat before increasing the spawn rate
    public float timeToStartSpawning = 1.0f;
    public float timeBetweenSpawnAttempts = 1.0f;

    public float spawnTimeDecrease = 1.0f;
    public float spawnTimeMin = 1.0f;
    public float spawnTimer;
    public float timeToIncreaseSpawnRate = 10.0f;

    public RadarDisplay radarScript;
    public GameObject radarPrefab;

    //private float spawnRoll;
    //private int count;


	void Start () {
        //InvokeRepeating("SpawnEnemy", timeToStartSpawning, timeBetweenSpawnAttempts);
        spawnTimer = 0f;
        radarScript = GameObject.FindObjectOfType(typeof(RadarDisplay)) as RadarDisplay;

        InvokeRepeating("SpawnControl", timeToStartSpawning, timeBetweenSpawnAttempts);
	}

    private void Update()
    {
        SpawnRateChangerChunkUpdate();
    }

    //void SpawnEnemy()
    //{
    //    count++;
    //    if (spawnRate < 1)
    //    {
    //        if (count >= countToIncreaseSpawnRate)
    //        {
    //            spawnRate += spawnIncrease;
    //            count = 0;
    //        }
    //    }
    //    spawnRoll = Random.Range(0f, 1f);
    //    if (spawnRoll <= spawnRate)
    //    {
    //        Instantiate(enemyObject, transform.position, Quaternion.identity);
    //    }
    //}

    public void SpawnControl()
    {
        List<Transform> freeSpawnPoints = new List<Transform>(spawns);  // Creates new array of the transform values of the list of objects dragged onto this script
        int index = Random.Range(0, freeSpawnPoints.Count);  // Picks a random index number between 0 and the size of the previous array (randomly selects transform from array)
        Transform selectedSpawn = freeSpawnPoints[index];
        GameObject enemyBeingSpawned =  Instantiate(enemyObject, selectedSpawn.position, selectedSpawn.rotation);  // Creates an object at the selected position

        // Add spawned enemy to the List in RadarDisplay script
        radarScript.radarObjects.Add(enemyBeingSpawned.transform.GetChild(1).gameObject);
        radarScript.borderObjects.Add(enemyBeingSpawned.transform.GetChild(2).gameObject);
        
        
    }

    void SpawnRateChanger()
    {
        if (timeBetweenSpawnAttempts > spawnTimeMin) // Do not want timeBetweenSpawnAttempts to go below the minimum value set
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= timeToIncreaseSpawnRate) // This will activate after timeToIncreaseSpawnRate time has passed
            {
                timeBetweenSpawnAttempts -= spawnTimeDecrease; // Decreases time between spawns by value indicated, which spawns enemies more rapidly
                spawnTimer = 0;
            }
        }
    }

    void SpawnRateChangerChunkInvoke()
    {
        if (spawnTimer >= timeToIncreaseSpawnRate) // This will activate after timeToIncreaseSpawnRate time has passed
        {
            timeBetweenSpawnAttempts -= spawnTimeDecrease; // Decreases time between spawns by value indicated, which spawns enemies more rapidly
            spawnTimer = 0;
        }
    }

    void SpawnRateChangerChunkUpdate()
    {
        if (timeBetweenSpawnAttempts > spawnTimeMin) // Do not want timeBetweenSpawnAttempts to go below the minimum value set
        {
            spawnTimer += Time.deltaTime;
        }
    }
}
