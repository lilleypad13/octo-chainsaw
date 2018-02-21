using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentProjectileOverlap : MonoBehaviour {

    SpriteRenderer sr;

    private Color old;

    public float timeToShowOverlap = 1.0f;
    public float scalingFactor = 1.0f;
    public Vector3 currentScale;
    public int projectileId;



    //private void Start()
    //{
    //    sr = GetComponent<SpriteRenderer>();
    //    old = sr.color;
    //    //currentScale = transform.localScale;
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    OverlappingProjectiles(other);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //OverlappingProjectiles(collision);
    //    //StartCoroutine(OverlappingProj(collision));
    //}

    //void OverlappingProjectiles(Collider2D projectile)
    //{
    //    if (projectile.gameObject.CompareTag("Projectile"))
    //    {
    //        sr.color = Color.red;
    //    }
    //}

    //IEnumerator OverlappingProj(Collider2D projectile)
    //{
    //    if (projectile.gameObject.CompareTag("Projectile")/* && projectile.GetComponent<SegmentProjectileOverlap>().projectileId != projectileId*/) // Check if colliding with another projectile, and that that projectile is a different instance (has a different projectileId value)
    //    {
    //        sr.color = Color.red;
    //    }
    //    yield return new WaitForSeconds(timeToShowOverlap);
    //    sr.color = old;
    //}

    private void Update()
    {
        ProjectileStretch();
    }

    void ProjectileStretch()
    {
        //currentScale += new Vector3(Time.deltaTime * scalingFactor, 0);
        //transform.localScale = Vector3.Scale(transform.localScale, new Vector3(scalingFactor, 0, 0));
        transform.localScale += new Vector3(0.1f * Time.deltaTime, 0, 0);
    }

}
