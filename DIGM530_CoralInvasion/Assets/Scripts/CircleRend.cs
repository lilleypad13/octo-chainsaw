using UnityEngine;
using System.Collections;

public class CircleRend : MonoBehaviour
{
    public Transform playerpos;
    public GameObject projectile;
    public float xscale = 0;
    public float yscale = 0;
    public ParticleSystem myParticle;
    public int seconds;
    private bool _slowActive;

    void Start()
    {

        projectile.SetActive(false);
        _slowActive = true;
        var emissionEnabled = myParticle.emission;
        emissionEnabled.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButton("Fire3") && _slowActive == true)
        {
            //projectile.SetActive(true);
            ActivateProj();
            var emissionEnabled = myParticle.emission;
            emissionEnabled.enabled = false;
            projectile.transform.position = playerpos.position;
            CheckRange();

        }

        if (Input.GetButtonUp("Fire3"))
        {

            var emissionEnabled = myParticle.emission;
            emissionEnabled.enabled = true;


            var particleShape = myParticle.shape;
            particleShape.radius = xscale / 2;

            print(particleShape);
            //Debug.Log("space release");
            xscale = 0.0f;
            yscale = 0.0f;
            DeactivateProj();

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

    public void DeactivateProj()
    {
        StartCoroutine(RemoveAfterSeconds(10));
    }

    IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        projectile.SetActive(false);
    }
}