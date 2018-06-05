using UnityEngine;

public class SpawnPointEnemyScript : MonoBehaviour {

    #region Variables
    bool isActive;

    SpawnEnemyScript spawnEnemyScript;

    public GameObject spawnManager;
    #endregion

    #region Unity Methods

    private void Start()
    {
        spawnEnemyScript = spawnManager.GetComponent<SpawnEnemyScript>();
        isActive = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive == false)
        {
            if (collision.gameObject.CompareTag("Obstacles"))
            {
                return;
            }
            else
            {
                isActive = true;
                Debug.Log(name + " is active: " + isActive);
                spawnEnemyScript.currentSpawnPoints.Add(gameObject.transform);
                Debug.Log(name + " has been added to list " + spawnEnemyScript.currentSpawnPoints);
            }
        }
        
    }

    #endregion
}
