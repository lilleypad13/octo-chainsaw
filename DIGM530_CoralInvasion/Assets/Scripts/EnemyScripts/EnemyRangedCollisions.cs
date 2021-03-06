﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCollisions : MonoBehaviour {

    public float startingHealth = 3.0f;
    public float currentHealth;
    public float damageScalingFactor = 1.0f;
    public float overlapMultiplier = 1.0f;
    public float lastProjectileHitBy;
    public int numberOfProjectiles; // Determines how many projectile colliders the enemy is currently within

    private int layerMask;

    public float attackRange;
    public int damage;
    public float lastAttackTime;
    public float attackDelay;
    private Transform target;
    public float chaseRange;

    public GameObject projectile;
    public float projectileForce;

    private SpriteRenderer sr;
    private Color oldColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        GameObject go = GameObject.FindGameObjectWithTag("Base");
        target = go.transform;
    }

    void Start()
    {
        oldColor = sr.color;
        //GameObject go = GameObject.FindGameObjectWithTag("Base");
        //target = go.transform;
        numberOfProjectiles = 0;
        layerMask = 1 << 10;
        //layerMask = ~layerMask;
        
    }

    private void Update()
    {

        ApplyDamage();
        float distanceToTarget = Vector3.Distance(transform.position, target.position); // Checks distance between this object and target's position
        if (distanceToTarget <= attackRange)
        {
            // Stop enemy once it gets in range of target
            // Need to add a way to get enemy started again for cases of moving targets
            transform.parent.gameObject.GetComponent<EnemySmartMovement>().currentMoveSpeed = 0;

            //Turn towards target
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; // May need 90 deg offset
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward); //May want forward instead of right
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

            //Check to see if it's time to attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                //Debug.Log("Enemy has reloaded and is able to fire.");
                //Raycast to see if we have line of sight to the target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, layerMask); //Need layer mask to keep raycast from hitting self or parent object
                //Check to see if we hit anything and what it was
                Debug.Log("Transform value of target: " + target);
                Debug.Log("Transform value of raycast hit: " + hit.transform);
                if (hit.transform == target)
                {
                    Debug.Log("Raycast hit target");
                    //Hit target - fire the projectile
                    GameObject newProj = Instantiate(projectile, transform.position, transform.rotation);
                    newProj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileForce, 0f)); // May want to put force in different direction
                    lastAttackTime = Time.time;
                }
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
