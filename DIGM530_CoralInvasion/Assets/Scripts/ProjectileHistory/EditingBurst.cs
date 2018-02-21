using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingBurst : MonoBehaviour {

    public int numberOfShots = 20;
    public float projectileDuration = 2.0f;
    public Transform projectileSpawn;
    public GameObject projectile; //what to shoot
    public float projectileSpeed = 2.0f;
    public float[] angles = new float[] { -40f, -35f, -25f, -10f, 0, 10f, 25f, 35f, 40f };

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        //We'll only ever fire once with this condition at a speed of 1
        //And we'll fire at the exact same time every pass as the fire rate is also 1
        //Rotating the shot position and orientation is easier if they are aligned
        //get the distance from the center
        //I assume this is to avoid the projectile overlapping the object
        //and uses scale to appropriately scale as the object scales
        //Vector3 localShotPos = new Vector3(0, -((new Vector2(transform.localScale.x * 8f,
        //                               transform.localScale.y * 5f)).magnitude));
        //foreach (float angle in angles)
        //{
        //    Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
        //    Vector3 shotPosition = transform.position + rotation * localShotPos;
        //    Instantiate(Projectile, shotPosition, rotation * transform.rotation);
        //}
        float degree = 360f / numberOfShots;
        for (float i = -180f; i < 180f; i += degree)
        {
            //Quaternion rotation = Quaternion.AngleAxis(i, transform.forward);
            Vector3 angle = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0f);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, angle);
            //transform.rotation = rotation;
            Vector3 shotPosition = projectileSpawn.position + angle * 100;
            //Vector3 shotPosition = transform.position + rotation * localShotPos;
            var bullet = (GameObject)Instantiate(projectile, shotPosition, transform.rotation * rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = angle * projectileSpeed;
            Destroy(bullet, projectileDuration);
            //Instantiate(projectile, shotPosition, rotation * transform.rotation);
            //Instantiate(projectile, shotPosition, Quaternion.FromToRotation(Vector3.up, rotation * transform.rotation);
            //projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.forward * projectileSpeed;
        }
    }

}
