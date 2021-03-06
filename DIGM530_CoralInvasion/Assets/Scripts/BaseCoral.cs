﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoral : MonoBehaviour {

    public float growthSpeed = 1.0f;
    public int currentHealth;
    public int maxHealth = 10;
    public int generalEnemyDamage = 1;
    public int generalEnemyProjDamage = 1;
    public float winScale = 2.0f;
    public float growthLostOnGeneralHit = 0.1f;
    public float growthLostOnProjHit = 0.1f;
    public AudioClip baseDamageSound;

    private AudioSource audioController;
    private Animator anim;
    private int takeDamageHash = Animator.StringToHash("TakeDamage");


	void Start () {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        audioController = GetComponent<AudioSource>();
	}

    // The win condition for the game has been changed, so these can be disregarded for now.
    //private void Update()
    //{
    //    IncreaseSize();
    //    WinCondition();
    //}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        HitByEnemy(coll);
        HitByEnemyProjectile(coll);
    }

    private void HitByEnemy(Collider2D damagingObject)
    {
        if (damagingObject.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Scale of base before being hit is: " + transform.parent.localScale);
            currentHealth -= generalEnemyDamage;

            // Causes coral to shrink an amount equal to growthLostOnGeneralHit
            transform.parent.localScale -= new Vector3(growthLostOnGeneralHit, growthLostOnGeneralHit, 0f);

            audioController.PlayOneShot(baseDamageSound, 1.0f);

            // Coral blinks upon taking damage
            anim.SetTrigger(takeDamageHash);

            // Sets lose condition of having the coral no longer exist (scale has dropped to or below 0, so the sprite is gone or reversed)
            if (transform.parent.localScale.x <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }

            if (currentHealth <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }
            Debug.Log("New scale of base after hit is: " + transform.parent.localScale);
        }
    }

    private void HitByEnemyProjectile(Collider2D enemyProj)
    {
        if (enemyProj.gameObject.CompareTag("EnemyProjectile"))
        {
            currentHealth -= generalEnemyProjDamage; // Says anytime this object collides with an object tagged "EnemyProjectile", the currentHealth of this
            // object will be reduced by the value of generalEnemyProjDamage
            //Debug.Log("The coral has " + currentHealth + " health left.");

            transform.parent.localScale -= new Vector3(growthLostOnProjHit, growthLostOnProjHit, 0f); // Causes coral to shrink an amount equal to growthLostOnProjHit

            anim.SetTrigger(takeDamageHash); // Coral blinks upon taking damage

            // Sets lose condition of having the coral no longer exist (scale has dropped to or below 0, so the sprite is gone or reversed)
            if (transform.parent.localScale.x <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }

            if (currentHealth <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }
        }
    }

    void IncreaseSize()
    {
        transform.parent.localScale += new Vector3(growthSpeed, growthSpeed, 0f) * Time.deltaTime;
    }

    void WinCondition()
    {
        if (transform.parent.localScale.x >= winScale) //Only checks scale on the x-axis, but should be the same all around
        {
            FindObjectOfType<GameManagerScript>().CompleteLevel();
        }
    }
}
