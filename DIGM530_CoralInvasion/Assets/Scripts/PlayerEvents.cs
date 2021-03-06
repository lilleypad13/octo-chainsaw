﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour {

    private Animator anim;
    private AudioSource audioControl;
    //private float vollowRange = .1f;
    //private float volHighRange = 2.0f;

    public AudioClip SoundBurst;
    public AudioClip playerDamageSound;
    public AudioClip pickedUpResourceSound;
    public AudioClip droppedOffResourceSound;
    public bool isInvincible;
    public float timeForInvincibilityFrames = 1.0f;
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
    public float speedInWaste = 500f;
    public GameObject baseObject;
    public int inventoryMaximum = 3;

    public GameObject[] inventoryUIVisuals;

    private int inventoryResource;
    private float initialSpeed;
    //public ParticleSystem projectileParticle;

    private void Awake()
    {
        audioControl = GetComponent<AudioSource>();
    }

    void Start () {
        isInvincible = false;
        projectileIdentifier = 0;
        currentHealth = maxHealth; // Initializes currentHealth as the value set for maxHealth
        anim = this.GetComponent<Animator>();
        reloadTimer = timeToReload; // Circumvents reload time to start the game
        initialSpeed = transform.parent.gameObject.GetComponent<PlayerController>().speed;
        inventoryResource = 0;
    }

    private void Update()
    {
        // Solely here to be used to set the walking animation
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        float pressdowntimer = 0.0f;
        bool fireslowmo = false;

        if (reloadTimer < timeToReload) // Sets an amount of time where the player cannot use their weapon, to prevent mindless spamming
        {
            reloadTimer += Time.deltaTime;
        }
        if(pressdowntimer == 5.0f)
        {
            fireslowmo = true;
            Debug.Log(fireslowmo);
        }
        if (Input.GetButtonDown("Jump") && reloadTimer >= timeToReload) // && fireslowmo) // Player can only fire once timeToReload is met
        {
            FireRing();
            //FireRingParticle();
            anim.SetTrigger("PerformAttack");
            //float vol = Random.Range(vollowRange, volHighRange);
            audioControl.PlayOneShot(SoundBurst, 1f);
            reloadTimer = 0f; // Resets the reloadTimer to start over
            pressdowntimer++;
            Debug.Log(pressdowntimer);
            //pressdowntimer = 0.0f;
            //fireslowmo = false;
        }
        anim.SetFloat("MoveSpeed", movement.magnitude);

        if (inventoryResource > 0)
        {
            inventoryUIVisuals[0].SetActive(true);
            if (inventoryResource > 1)
            {
                inventoryUIVisuals[1].SetActive(true);
            }
            else
            {
                inventoryUIVisuals[1].SetActive(false);
                inventoryUIVisuals[2].SetActive(false);
            }
                if (inventoryResource > 2)
                {
                    inventoryUIVisuals[2].SetActive(true);
                }
                else
                {
                inventoryUIVisuals[2].SetActive(false);
                }
        }
        else
        {
            inventoryUIVisuals[0].SetActive(false);
            inventoryUIVisuals[1].SetActive(false);
            inventoryUIVisuals[2].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        TakeDamage(coll);
        WhileInToxic(coll);
        PickupResource(coll);
        EnteredDropoffZone(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        LeaveToxic(coll);
    }

    void FireRing()
    {
        Vector3 shotPosition = projectileSpawn.position;
        var bullet = Instantiate(projectile, shotPosition, Quaternion.identity); // Creates a new gameObject set to the new variable of bullet
        //var bulletVisual = Instantiate(projectileVisual, shotPosition, Quaternion.identity); // This creates the visual effect along with the collider
        StartCoroutine(SecondaryProjectile(shotPosition)); // Fires a second projectile from the same location as the initial projectile, but it starts later
        //bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier; // Assigns projectileId of instantiated bullets with current projectilIdentifier
        Destroy(bullet, projectileDuration); // Removes the bullet gameObject fired after projectileDuration time has passed
        projectileIdentifier++; // Moves to a different projectileIdentifier value so that the next projectile will have a different identifier (may not be necessary)
    }

    void FireRingParticle()
    {
        Vector3 shotPosition = projectileSpawn.position;
        var bullet = Instantiate(projectile, shotPosition, Quaternion.identity); // Creates a new gameObject set to the new variable of bullet
        StartCoroutine(SecondaryProjectile(shotPosition)); // Fires a second projectile from the same location as the initial projectile, but it starts later
        bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier; // Assigns projectileId of instantiated bullets with current projectilIdentifier
        Destroy(bullet, projectileDuration); // Removes the bullet gameObject fired after projectileDuration time has passed
        //projectileParticle.Play();
        projectileIdentifier++; // Moves to a different projectileIdentifier value so that the next projectile will have a different identifier (may not be necessary)
    }

    IEnumerator SecondaryProjectile(Vector3 projectileOrigin)
    {
        yield return new WaitForSeconds(antiProjectileDelay);
        var bullet = Instantiate(antiProjectile, projectileOrigin, Quaternion.identity); // Same as above, but this exists to fire a secondary projectile later than the first
        //bullet.GetComponent<CircularSoundProjectileScript>().projectileId = projectileIdentifier;
        Destroy(bullet, projectileDuration);
    }

    void TakeDamage (Collider2D enemy)
    {
        if (enemy.gameObject.CompareTag("Enemy") && isInvincible == false)
        {
            isInvincible = true;
            currentHealth--;
            audioControl.PlayOneShot(playerDamageSound, 1f);
            anim.SetTrigger("TakeDamage");
            if (currentHealth <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }
            Debug.Log("Player Is Invincible" + isInvincible);
            Invoke("ResetInvulnerability", timeForInvincibilityFrames);
        }
    }

    void WhileInToxic(Collider2D waste)
    {
        if (waste.gameObject.CompareTag("Obstacles"))
        {
            transform.parent.gameObject.GetComponent<PlayerController>().speed = speedInWaste;
        }
    }

    void LeaveToxic(Collider2D waste)
    {
        if (waste.gameObject.CompareTag("Obstacles"))
        {
            transform.parent.gameObject.GetComponent<PlayerController>().speed = initialSpeed;
        }
    }

    void PickupResource(Collider2D resource)
    {
        if (resource.gameObject.CompareTag("ResourcePickup") && inventoryResource < inventoryMaximum)
        {
            inventoryResource++;
            Debug.Log("Player is holding " + inventoryResource + " resources.");
            audioControl.PlayOneShot(pickedUpResourceSound, 1f);
            Destroy(resource.gameObject);
            Debug.Log("Resource has been destroyed and collected by player.");
        }

    }

    void EnteredDropoffZone(Collider2D dropoffZone)
    {
        if (dropoffZone.gameObject.CompareTag("Dropoff") && inventoryResource > 0)
        {
            baseObject.GetComponentInChildren<BaseResourceManagement>().resourceStockpile += inventoryResource;
            inventoryResource = 0;
            audioControl.PlayOneShot(droppedOffResourceSound, 1f);
        }
    }


    void ResetInvulnerability()
    {
        isInvincible = false;
        Debug.Log("Player Is Invincible" + isInvincible);
    }

}
