using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resonance : MonoBehaviour {

    public float projectileDuration = 2.0f;
    public GameObject projectile; //what to shoot
    //public ParticleSystem projectileVisual; // Visual effects for the projectile
    public GameObject antiProjectile; // Secondary ring that serves as "empty space" behind initial circle to create a ring effect
    public float antiProjectileDelay = 1.0f;
    public float timeToReactivateHitbox = 5.0f;

    private AudioSource source;
    public AudioClip SoundBurst;

    private BoxCollider2D hitbox;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color currentColor;
    private float reactivateHitboxTimer;

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        reactivateHitboxTimer = 0;
    }

    private void FixedUpdate()
    {
        reactivateHitboxTimer += Time.deltaTime;
    }

    //private void Update()
    //{
    //    if (hitbox.enabled == false)
    //    {
    //        if (reactivateHitboxTimer <= timeToReactivateHitbox)
    //        {
    //            reactivateHitboxTimer += Time.deltaTime;
    //        }
    //        else
    //        {
    //            hitbox.enabled = true;
    //            reactivateHitboxTimer = 0f;
    //        }

    //    }
    //}

    void OnTriggerEnter2D (Collider2D coll)
    {
        HitByProjectile(coll);
        //Debug.Log("Hitbox is on = " + hitbox.enabled + " at time stamp " + reactivateHitboxTimer);
    }

    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    HitByProjectile(coll);
    //}

    void HitByProjectile(Collider2D proj)
    {
        if (proj.gameObject.CompareTag("Projectile"))
        {
            FireRing();
            hitbox.enabled = false;
            spriteRenderer.color = Color.red;
            Debug.Log("Hitbox is on = " + hitbox.enabled + " at time stamp " + reactivateHitboxTimer);
            StartCoroutine(TurnOnHitBox());
        }
    }

    IEnumerator TurnOnHitBox()
    {
        yield return new WaitForSeconds(timeToReactivateHitbox);
        hitbox.enabled = true;
        spriteRenderer.color = originalColor;
        Debug.Log("Hitbox is on = " + hitbox.enabled + " at time stamp " + reactivateHitboxTimer);
    }



    void FireRing()
    {
        Vector3 shotPosition = transform.position;
        source.PlayOneShot(SoundBurst, 1f);
        var bullet = Instantiate(projectile, shotPosition, Quaternion.identity); // Creates a new gameObject set to the new variable of bullet
        //var bulletVisual = Instantiate(projectileVisual, shotPosition, Quaternion.identity); // This creates the visual effect along with the collider
        StartCoroutine(SecondaryProjectile(shotPosition)); // Fires a second projectile from the same location as the initial projectile, but it starts later
        Destroy(bullet, projectileDuration); // Removes the bullet gameObject fired after projectileDuration time has passed
        Debug.Log("Projectile fired from resonance object.");
    }

    IEnumerator SecondaryProjectile(Vector3 projectileOrigin)
    {
        yield return new WaitForSeconds(antiProjectileDelay);
        var bullet = Instantiate(antiProjectile, projectileOrigin, Quaternion.identity); // Same as above, but this exists to fire a secondary projectile later than the first
        Destroy(bullet, projectileDuration);
    }

}
