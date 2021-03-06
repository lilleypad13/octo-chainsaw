﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour {

    public float startingHealth = 3.0f;
    public float currentHealth;
    public float damageScalingFactor = 1.0f;
    public float overlapMultiplier = 1.0f;
    //public GameObject slowprojectile;
    public float lastProjectileHitBy;
    public int numberOfProjectiles; // Determines how many projectile colliders the enemy is currently within
    public GameObject healthBarVisual;
    public AudioClip enemyDamageSound;

    //private AudioSource audioControl;
    private Transform healthBar;
    private Vector3 startingHealthBarScale;
    private Vector3 currentHealthBarScale;

    private SpriteRenderer sr;
    private Color oldColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //audioControl = GetComponent<AudioSource>();

        healthBar = gameObject.transform.GetChild(0);
        startingHealthBarScale = healthBar.localScale;
        currentHealthBarScale = startingHealthBarScale;
        //Debug.Log("Enemy starting health scale: " + startingHealthBarScale);

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
        ScaleHealthBarEnemy();
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
            AudioSource.PlayClipAtPoint(enemyDamageSound, transform.position);
            Debug.Log("Enemy made sound " + enemyDamageSound.name);
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

    void ScaleHealthBarEnemy()
    {
        // Applies the proportion of the enemy's current health to full health and applies that to whatever the initial scale
        // the health bar started at to have a similarly proportioned current scale.
        currentHealthBarScale.x = startingHealthBarScale.x * currentHealth / startingHealth;
        healthBar.transform.localScale = currentHealthBarScale;
        //Debug.Log("Scale of the health bar is: " + currentHealthBarScale.x);
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

    void HitBySlowProjectile(Collider2D slow)
    {
        if (slow.gameObject.CompareTag("SlowProj"))
        {
            GetComponentInParent<EnemySmartMovement>().currentMoveSpeed = GetComponentInParent<EnemySmartMovement>().currentMoveSpeed;
        }
    }

}
