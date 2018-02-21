using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleBurstPattern : MonoBehaviour {

    public float MinSpeed = 1f; //min speed to move down at
    public float MaxSpeed = 1f; //max speed to move down at
    public int numberOfShots = 20;

    public float currentSpeed; //speed to move down at
    public Transform Projectile; //what to shoot
    public float[] angles = new float[] { -40f, -35f, -25f, -10f, 0, 10f, 25f, 35f, 40f };
    private float x; //random x position
    float firingRate = 1.0f; //delay between shots, in seconds
    float lastFired = -100f; //absolute time of last fired shot

    void Start()
    { //Initialization
        currentSpeed = Random.Range(MinSpeed, MaxSpeed); //Random down speed
        x = Random.Range(0f, 1f); //random x position
        transform.position = new Vector3(x, 7.0f, 0.0f);
    }

    void Update()
    {
        float amtToMove = currentSpeed * Time.deltaTime; //how far to move this frame
        transform.Translate(-Vector3.up * amtToMove); //move down
        if (Time.time < lastFired + firingRate) {
            return;
        }
        //we just fired
        //reset position if we've fallen too far
        //You might consider a level manager script elsewhere to destroy enemies
        //that go off the screen and spawn new ones as appropriate
        if (transform.position.y <= -6.0) {
            currentSpeed = Random.Range(MinSpeed, MaxSpeed);
            transform.position = new Vector3(x, 7.0f, 0.0f);
        }
        lastFired = Time.time; //We're firing
                               //If we're off the screen, should we really be firing?
        if (transform.position.y < 0f || transform.position.y > 1f) return;
        //We'll only ever fire once with this condition at a speed of 1
        //And we'll fire at the exact same time every pass as the fire rate is also 1
        //Rotating the shot position and orientation is easier if they are aligned
        //get the distance from the center
        //I assume this is to avoid the projectile overlapping the object
        //and uses scale to appropriately scale as the object scales
        Vector3 localShotPos = new Vector3(0, -((new Vector2(transform.localScale.x * 8f,
                                       transform.localScale.y * 5f)).magnitude));
        //Since your scene appears to only be 1-7 units large,
        //aren't these sizes (8 and 5) a little big?
        //foreach (float angle in angles)
        //{
        //    Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
        //    Vector3 shotPosition = transform.position + rotation * localShotPos;
        //    Instantiate(Projectile, shotPosition, rotation * transform.rotation);
        //}
        float degree = 360f / numberOfShots;
        for (float i = -180f; i < 180f; i += degree)
        {
            Quaternion rotation = Quaternion.AngleAxis(i, transform.forward);
            Vector3 shotPosition = transform.position + rotation * localShotPos;
            Instantiate(Projectile, shotPosition, rotation * transform.rotation);
        }

    }

}
