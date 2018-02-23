using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOldProj : MonoBehaviour {



    public float speed = 1.0f;
    //public GameObject projectile;
    //public GameObject projectileSpawn;

    private Rigidbody2D rb;

    public int projectileIdentifier; // Variable to give projectile shots identification number, so that they can determine if they collide with a different instance of a projectile
    public int numberOfShots = 20;
    public float projectileDuration = 2.0f;
    public Transform projectileSpawn;
    public GameObject projectile; //what to shoot
    public GameObject antiProjectile; // Secondary ring that serves as "empty space" behind initial circle to create a ring effect
    public float antiProjectileDelay = 1.0f;
    public float reloadTimer;
    public float timeToReload = 1.0f;
    //public float projectileSpeed = 2.0f;
    //public float[] angles = new float[] { -40f, -35f, -25f, -10f, 0, 10f, 25f, 35f, 40f };


    void Start () {
        projectileIdentifier = 0;
        rb = GetComponent<Rigidbody2D>();
        reloadTimer = timeToReload;
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);
    }

    private void Update()
    {
        if (reloadTimer < timeToReload) // Sets an amount of time where the player cannot use their weapon, to prevent mindless spamming
        {
            reloadTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && reloadTimer >= timeToReload)
        {
            //fireSegmentedProjectile();
            FireRing();
            reloadTimer = 0f;
        }
    }

    //void fireProjectile()
    //{
    //    Instantiate(projectile, projectileSpawn.transform.position, Quaternion.identity);
    //}

    //void Fire()
    //{
    //    // Create the Bullet from the Bullet Prefab
    //    var bullet = (GameObject)Instantiate(
    //        projectile,
    //        projectileSpawn.position,
    //        projectileSpawn.rotation);

    //    // Add velocity to the bullet
    //    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

    //    // Destroy the bullet after 2 seconds
    //    Destroy(bullet, 2.0f);
    //}

    //void FireSegmentedProjectile()
    //{
    //    float degree = 360f / numberOfShots;
    //    for (float i = -180f; i < 180f; i += degree)
    //    {
    //        //Quaternion rotation = Quaternion.AngleAxis(i, transform.forward);
    //        Vector3 angle = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0f);
    //        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, angle);
    //        //transform.rotation = rotation;
    //        Vector3 shotPosition = projectileSpawn.position;
    //        //Vector3 shotPosition = transform.position + rotation * localShotPos;
    //        var bullet = (GameObject)Instantiate(projectile, shotPosition, transform.rotation * rotation);
    //        bullet.GetComponent<Rigidbody2D>().velocity = angle * projectileSpeed;
    //        bullet.GetComponent<SegmentProjectileOverlap>().projectileId = projectileIdentifier; // Assigns projectileId of instantiated bullets with current projectilIdentifier
    //        Destroy(bullet, projectileDuration);
    //        //Instantiate(projectile, shotPosition, rotation * transform.rotation);
    //        //Instantiate(projectile, shotPosition, Quaternion.FromToRotation(Vector3.up, rotation * transform.rotation);
    //        //projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.forward * projectileSpeed;
    //    }
    //    projectileIdentifier++; // Basically just changes identifier variable so next projectile list will have a new value
    //    //Debug.Log("Current Projectile ID value is: " + projectileIdentifier);
    //}

    void FireRing()
    {
        Vector3 shotPosition = projectileSpawn.position;
        var bullet = (GameObject)Instantiate(projectile, shotPosition, Quaternion.identity);
        StartCoroutine(SecondaryProjectile(shotPosition));
        bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier; // Assigns projectileId of instantiated bullets with current projectilIdentifier
        Destroy(bullet, projectileDuration);
        projectileIdentifier++;
    }

    IEnumerator SecondaryProjectile(Vector3 projectileOrigin)
    {
        yield return new WaitForSeconds(antiProjectileDelay);
        var bullet = (GameObject)Instantiate(antiProjectile, projectileOrigin, Quaternion.identity);
        //bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier;
        Destroy(bullet, projectileDuration);
    }


}
