using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShot : MonoBehaviour {


    public GameObject projectile;
    public GameObject projectileSpawn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireProjectile();
        }
    }

    void fireProjectile()
    {
        Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
    }
}
