using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigCrystal : MonoBehaviour {

	
    public float startingHealth = 3.0f;
    public float currentHealth;
    public float damageScalingFactor = 1.0f;
    public float overlapMultiplier = 1.0f;
    public float lastProjectileHitBy;
    public int numberOfProjectiles; // Determines how many projectile colliders the enemy is currently within

    public Transform crystaltopick;

    private SpriteRenderer sr;
    private Color oldColor;
    private Vector3 originalposition;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }

    void Start()
    {
        oldColor = sr.color;
        numberOfProjectiles = 0;
    }

    private void Update()
    {
        ApplyDamage();
    }

    // Tests any collisions where attached object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D coll)
    {
        HitByProjectile(coll);
        MeleeHitBase(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Projectile")) // This will need to be edited for the case where enemy enteres first projectile, leaves first projectile, then comes back into second projectile
        {
            numberOfProjectiles--; // Covers case where enemy would leave original projectile somehow without getting hit by the secondary projectile
        }
    }

    // Used for melee enemies that will run directly into the coral base
    void MeleeHitBase(Collider2D baseCoral)
    {
       

        if (baseCoral.gameObject.CompareTag("Base"))
        {
            originalposition = transform.position;


           // Debug.Log("1111:"+originalposition);

            for (int i = 0; i < 3; i++)
            {
                Instantiate(crystaltopick,originalposition + new Vector3(i * 1.0f, i * 1.0f, 0), Quaternion.identity);
            }
            Destroy(transform.gameObject);
         
        }
    }

    void TakeDamageOverTimeUpdate(float damageMultiplier)
    {
        currentHealth -= Time.deltaTime * damageScalingFactor * damageMultiplier;
        if (currentHealth <= 0)
        {
            originalposition = transform.position;
            //originalposition = originalposition + new Vector3(1 * 0.5f, 1 * 0.5f, 0);
           // Debug.Log("22222:" + originalposition);
          
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log("222222:" + transform.position);
                Instantiate(crystaltopick, originalposition + new Vector3(i * 1.0f, i * 1.0f, 0), Quaternion.identity);
            }

            Destroy(transform.gameObject);
        }
    }

    void ApplyDamage()
    {
        if (numberOfProjectiles >= 1)
        {
            if (numberOfProjectiles >= 2)
            {
                sr.color = Color.red;
                TakeDamageOverTimeUpdate(overlapMultiplier);  // If in both projectiles, enemy will take increaseed damage based on overlapMultiplier value
            }
            else
            {
                sr.color = oldColor;
                TakeDamageOverTimeUpdate(1.0f);  // If only in one projectile, enemy will take normal damage (multiplier of 1.0)
            }
        }
    }

    void HitByProjectile(Collider2D proj)
    {
        if (proj.gameObject.CompareTag("Projectile"))
        {
            numberOfProjectiles++;
        }
        if (proj.gameObject.CompareTag("AntiProjectile"))
        {
            if (numberOfProjectiles >= 1)
            {
                numberOfProjectiles--;
            }
        }
}
}
