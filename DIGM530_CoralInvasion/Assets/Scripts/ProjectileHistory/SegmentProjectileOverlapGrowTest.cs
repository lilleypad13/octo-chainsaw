using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentProjectileOverlapGrowTest : MonoBehaviour {

    SpriteRenderer sr;

    private Color old;
    //private ArcCollider2DPoly colliderShape;

    public float timeToShowOverlap = 1.0f;
    //public float shapeScalingFactor = 1.0f;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //colliderShape = gameObject.GetComponent<ArcCollider2DPoly>();
        old = sr.color;
    }

    private void Update()
    {
        //colliderShape.radius += Time.deltaTime * shapeScalingFactor;
        //colliderShape.radiusSmall += Time.deltaTime * shapeScalingFactor;
        //Debug.Log("The collider radius is: " + colliderShape.radius);
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    OverlappingProjectiles(other);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //OverlappingProjectiles(collision);
        StartCoroutine(OverlappingProj(collision));
    }

    void OverlappingProjectiles(Collider2D projectile)
    {
        if (projectile.gameObject.CompareTag("Projectile"))
        {
            sr.color = Color.red;
        }
    }

    IEnumerator OverlappingProj(Collider2D projectile)
    {
        if (projectile.gameObject.CompareTag("Projectile"))
        {
            sr.color = Color.red;
        }
        yield return new WaitForSeconds(timeToShowOverlap);
        sr.color = old;
    }

}
