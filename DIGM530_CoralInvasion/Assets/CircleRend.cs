using UnityEngine;
using System.Collections;

public class CircleRend : MonoBehaviour
{
    public Transform playerpos;
    public GameObject projectile;
    //public int segments;
    public float xscale=0;
    public float yscale=0;
    public ParticleSystem myParticle;
    //public Transform AOE;
    //LineRenderer line;

    void Start()
    {
        //line = gameObject.GetComponent<LineRenderer>();
        //line.enabled = true;
        //line.SetVertexCount(segments + 1);
        //line.useWorldSpace = false;
        //CreatePoints();

        var emissionEnabled = myParticle.emission.enabled;
        //emissionEnabled = false;
        myParticle.enableEmission = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //line.transform.position= playerpos.position;
            //Instantiate(line, playerpos);
            //gameObject.GetComponent<PlayerController>().enabled = false;
            var emissionEnabled = myParticle.emission.enabled;
            //emissionEnabled = false;
            myParticle.enableEmission = false;
            CheckRange();
          

        }
        if (Input.GetKeyUp("space"))
        {
            //line.transform.position= playerpos.position;
            //Instantiate(line, playerpos);
            //gameObject.GetComponent<PlayerController>().enabled = false;


            //Debug.Log("space release");

            //myParticle.transform.localScale = new Vector2(xscale/10, yscale/10);
            //myParticle.Play();
            var emissionEnabled = myParticle.emission;
            emissionEnabled.enabled = true;
            //myParticle.enableEmission = true;

            var particleShape = myParticle.shape;
            particleShape.radius = xscale/5;

            print(particleShape);
            Debug.Log("space release");

        }

    }
   

    void CheckRange()
    {
        //Instantiate(projectile, playerpos);
        projectile.transform.localScale = new Vector2(xscale, yscale);
        //Debug.Log("this" + projectile.transform.localScale);
        xscale = xscale + 0.1f;
        yscale = yscale + 0.1f;

        //CreatePoints();

        //if (Mathf.Approximately(xscale, 10.0f))
        if (xscale > 20.0f)
        {

            print(xscale);


            xscale = 0.0f;
            yscale = 0.0f;



        }
    }
}
    /*void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments+1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }*/

  

