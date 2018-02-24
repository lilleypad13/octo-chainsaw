using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIOverall : MonoBehaviour {

    //public GameObject targetObject;
    //public Transform target;
    public float moveSpeed = 2.0f;
    public int rotationSpeed = 2;
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
    private Transform myTransform;
    private Transform target;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        myTransform = transform;
        currentHealth = startingHealth;
    }

    void Start()
    {
        oldColor = sr.color;
        GameObject go = GameObject.FindGameObjectWithTag("Base");
        target = go.transform;
        numberOfProjectiles = 0;
    }

    void FixedUpdate()
    {
        Vector3 dir = target.position - myTransform.position;
        //dir.z = 0.0f; // Only needed if objects don't share 'z' value
        if (dir != Vector3.zero)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.FromToRotation(Vector3.right, dir),
                rotationSpeed * Time.deltaTime);
        }

        //Move Towards Target
        myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
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




        //if (inFirstProjectile == true)
        //{
        //    if (inSecondProjectile == true)
        //    {
        //        TakeDamageOverTimeUpdate(overlapMultiplier); // If in both projectiles, enemy will take increaseed damage based on overlapMultiplier value
        //    }
        //    else
        //    {
        //        TakeDamageOverTimeUpdate(1.0f); // If only in one projectile, enemy will take normal damage (multiplier of 1.0)
        //    }
        //}
    }

    // Tests any collisions where attached object enters a trigger collider
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Projectile"))
        {
            numberOfProjectiles++;
        }
        if (coll.gameObject.CompareTag("AntiProjectile"))
        {
            if (numberOfProjectiles >= 1)
            {
                numberOfProjectiles--;
            }
        }
        //if (inFirstProjectile == true && lastProjectileHitBy != coll.GetComponent<CircularSoundProjectileScript>().projectileId)
        //{
        //    HitByNextProj(coll);
        //}
        //HitByFirstProj(coll);
        MeleeHitBase(coll);
        //TakeDamage(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Projectile")) // This will need to be edited for the case where enemy enteres first projectile, leaves first projectile, then comes back into second projectile
        {
            numberOfProjectiles--; // Covers case where enemy would leave original projectile somehow without getting hit by the secondary projectile
        }
        //ExitedProjectile(coll);
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    TakeDamageOverTime(other);
    //}

    // Used for melee enemies that will run directly into the coral base
    void MeleeHitBase(Collider2D baseCoral)
    {
        if (baseCoral.gameObject.CompareTag("Base"))
        {
            Destroy(gameObject);
        }
    }

    void HitByFirstProj(Collider2D proj1)
    {
        if (proj1.gameObject.CompareTag("Projectile"))
        {
            inFirstProjectile = true;
            lastProjectileHitBy = proj1.GetComponent<CircularSoundProjectileScript>().projectileId;
        }
    }

    void HitByNextProj(Collider2D proj1)
    {
        if (proj1.gameObject.CompareTag("Projectile"))
        {
            inSecondProjectile = true;
            sr.color = Color.red;
            Debug.Log("Enemy in overlap.");
        }
    }

    // Code may need evaluated to deal with cases involving more than two possible colliders at one time
    void ExitedProjectile(Collider2D proj1)
    {
        if (proj1.gameObject.CompareTag("Projectile")) // Determines if player left a trigger with tag "projectile"
        {
            if (inSecondProjectile == true)
            {
                inSecondProjectile = false;  // If inSecondProjectile was true, this means it would leave one projectile, but still be in another
                sr.color = oldColor;
            }
            else
            {
                inFirstProjectile = false; // If inSecondProjectile is already false, then the object can only be in a single/first projectile
            }
        }
    }

    //void TakeDamage(Collider2D playerProjectile)
    //{
    //    if (playerProjectile.gameObject.CompareTag("Projectile"))
    //    {
    //        currentHealth -= 1;
    //        //Debug.Log("Enemy " + gameObject.name + "has been hit, and has: " + currentHealth + "health");
    //        if (currentHealth <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    void TakeDamageOverTime(Collider2D playerProj)
    {
        if (playerProj.gameObject.CompareTag("Projectile"))
        {
            currentHealth -= Time.deltaTime * damageScalingFactor * overlapMultiplier;
            //Debug.Log("Enemy " + gameObject.name + "has been hit, and has: " + currentHealth + "health");
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Enemy Destroyed");
            }
        }
    }

    void TakeDamageOverTimeUpdate(float damageMultiplier)
    {
        currentHealth -= Time.deltaTime * damageScalingFactor * damageMultiplier;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
