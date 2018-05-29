using UnityEngine;
using System.Collections;

public class CircleRend : MonoBehaviour
{
    public Transform playerpos;
    public GameObject projectile;
    public float xscale = 0;
    public float yscale = 0;
    public ParticleSystem myParticle;

    public bool timer;
    public bool timer2;
    private float timeStamp;
    public float coolDownPeriodinSeconds = 2.5f;


    void Start()
    {

        projectile.SetActive(false);
        timer = true;
        timer2 = true;
        var emissionEnabled = myParticle.emission;
        emissionEnabled.enabled = false;

    }

    private void Update()
    {
        //print(timeStamp);
        if (Input.GetButton("Fire3") && timer == true && timeStamp == 0)
        {

            ActivateProj();
            var emissionEnabled = myParticle.emission;
            emissionEnabled.enabled = false;
            projectile.transform.position = playerpos.position;
            CheckRange();
            timer2 = true;

        }

        if (Input.GetButtonUp("Fire3") && timer2 == true)
        {

            var particleShape = myParticle.shape;
            particleShape.radius = xscale / 2;
            var emissionEnabled = myParticle.emission;
            emissionEnabled.enabled = true;
            print(particleShape);
            xscale = 0.0f;
            yscale = 0.0f;
            timer = false;
            timer2 = false;
            timeStamp = coolDownPeriodinSeconds;

        }
        //print(timeStamp);

        if (timer2 == false)
        {
            timeStamp -= Time.deltaTime;
            if (timeStamp < 0)
            {
                projectile.SetActive(false);
               // print("power");
                timer = true;
                timeStamp = 0;
            }
        }


    }
    void CheckRange()
    {

        projectile.transform.localScale = new Vector2(xscale, yscale);
        xscale = xscale + 0.1f;
        yscale = yscale + 0.1f;

        if (xscale > 20.0f)
        {
            print(xscale);
            xscale = 0.0f;
            yscale = 0.0f;

        }
    }


    public void ActivateProj()
    {
        projectile.SetActive(true);
    }


}