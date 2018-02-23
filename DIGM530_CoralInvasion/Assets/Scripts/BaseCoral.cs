using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoral : MonoBehaviour {

    public int currentHealth;
    public int maxHealth = 10;
    public int generalEnemyDamage = 1;
    Animator anim;
    int takeDamageHash = Animator.StringToHash("TakeDamage");


	void Start () {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        HitByEnemy(coll);
    }

    private void HitByEnemy(Collider2D damagingObject)
    {
        if (damagingObject.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= generalEnemyDamage; // Says anytime this object collides with an object tagged "Enemy", the currentHealth of this
            // object will be reduced by the value of generalEnemyDamage
            Debug.Log("The coral has " + currentHealth + " health left.");
            anim.SetTrigger(takeDamageHash);
            if (currentHealth <= 0)
            {
                FindObjectOfType<GameManagerScript>().LoseGame();
            }
        }
    }
}
