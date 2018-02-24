using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerReference : MonoBehaviour {



    public float speed = 1.0f;

    private Animator anim;
    private Rigidbody2D rb;

    public int maxHealth = 5;
    public int currentHealth;
    public int projectileIdentifier; // Variable to give projectile shots identification number, so that they can determine if they collide with a different instance of a projectile
    public float projectileDuration = 2.0f;
    public Transform projectileSpawn;
    public GameObject projectile; //what to shoot
    public GameObject antiProjectile; // Secondary ring that serves as "empty space" behind initial circle to create a ring effect
    public float antiProjectileDelay = 1.0f;
    public float reloadTimer;
    public float timeToReload = 1.0f;

    private Vector3 facingDirection;


    void Start () {
        projectileIdentifier = 0;
        currentHealth = maxHealth; // Initializes currentHealth as the value set for maxHealth
        rb = GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        facingDirection = transform.localScale;
        reloadTimer = timeToReload; // Circumvents reload time to start the game
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);
        if (moveHorizontal > 0) // Need to fix so if moving in non facingDirection, player returns to idle in that direction. Currently it is always
            // in idle in facingDirection because it is the only state that includes moveHorizontal value of 0.
        {
            transform.localScale = new Vector3(facingDirection.x * -1, facingDirection.y);
        }
        else
        {
            transform.localScale = facingDirection;
        }
        anim.SetFloat("MoveSpeed", movement.magnitude);
    }

    private void Update()
    {
        if (reloadTimer < timeToReload) // Sets an amount of time where the player cannot use their weapon, to prevent mindless spamming
        {
            reloadTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && reloadTimer >= timeToReload) // Player can only fire once timeToReload is met
        {
            FireRing();
            anim.SetTrigger("PerformAttack");
            reloadTimer = 0f; // Resets the reloadTimer to start over
        }
    }

    void FireRing()
    {
        Vector3 shotPosition = projectileSpawn.position;
        var bullet = Instantiate(projectile, shotPosition, Quaternion.identity); // Creates a new gameObject set to the new variable of bullet
        StartCoroutine(SecondaryProjectile(shotPosition)); // Fires a second projectile from the same location as the initial projectile, but it starts later
        bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier; // Assigns projectileId of instantiated bullets with current projectilIdentifier
        Destroy(bullet, projectileDuration); // Removes the bullet gameObject fired after projectileDuration time has passed
        projectileIdentifier++; // Moves to a different projectileIdentifier value so that the next projectile will have a different identifier (may not be necessary)
    }

    IEnumerator SecondaryProjectile(Vector3 projectileOrigin)
    {
        yield return new WaitForSeconds(antiProjectileDelay);
        var bullet = Instantiate(antiProjectile, projectileOrigin, Quaternion.identity); // Same as above, but this exists to fire a secondary projectile later than the first
        //bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier;
        Destroy(bullet, projectileDuration);
    }


}
