using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {

    public float startingHealth = 3.0f;
    public float currentHealth;
    public float damageScalingFactor = 1.0f;
    public float overlapMultiplier = 1.0f;
    public float lastProjectileHitBy;

    public bool inFirstProjectile; // Determine how many overlapping projectiles enemy is in
    public bool inSecondProjectile;

    public int numberOfProjectiles; // Determines how many projectile colliders the enemy is currently within

    private SpriteRenderer sr;
    private Color oldColor;

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
            Destroy(transform.parent.gameObject);
        }
    }

    void TakeDamageOverTimeUpdate(float damageMultiplier)
    {
        currentHealth -= Time.deltaTime * damageScalingFactor * damageMultiplier;
        if (currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject);
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
