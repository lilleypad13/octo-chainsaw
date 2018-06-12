using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour {

    public GameObject enemyObject;
    public Transform[] spawns;
    public List<Transform> currentSpawnPoints = new List<Transform>();

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

    //private float spawnRoll;
    //private int count;


	void Start () {
        //InvokeRepeating("SpawnEnemy", timeToStartSpawning, timeBetweenSpawnAttempts);
        spawnTimer = 0f;
        //InvokeRepeating("SpawnControl", timeToStartSpawning, timeBetweenSpawnAttempts);
        InvokeRepeating("SpawnControlAdaptive", timeToStartSpawning, timeBetweenSpawnAttempts);
    }

    public void SpawnControl()
    {
        List<Transform> freeSpawnPoints = new List<Transform>(spawns);  // Creates new array of the transform values of the list of objects dragged onto this script
        int index = Random.Range(0, freeSpawnPoints.Count);  // Picks a random index number between 0 and the size of the previous array (randomly selects transform from array)
        Transform selectedSpawn = freeSpawnPoints[index];
        Instantiate(enemyObject, selectedSpawn.position, selectedSpawn.rotation);  // Creates an object at the selected position
        //SpawnRateChangerChunkInvoke();
    }

    void SpawnControlAdaptive()
    {
        //List<Transform> freeSpawnPoints = new List<Transform>(spawns);  // Creates new array of the transform values of the list of objects dragged onto this script
        int index = Random.Range(0, currentSpawnPoints.Count);  // Picks a random index number between 0 and the size of the previous array (randomly selects transform from array)
        Transform selectedSpawn = currentSpawnPoints[index];
        Instantiate(enemyObject, selectedSpawn.position, selectedSpawn.rotation);  // Creates an object at the selected position
    }

}
