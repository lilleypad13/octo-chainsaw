using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHealth : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;

    // Attach this type script to an object that represents a progress bar/meter of some kind.
    // This will scale that object from 0 - 100% based on the currentHealth variable (how close player is to goal) and the maxHealth value (the actual goal value to be reached).
    // The script being searched for to find the correct variables will need changed, as well as the names of said variables.
    // Could use efficiency updates

    void Start()
    {
        //baseCoral = GetComponent<BaseCoral>();
        //maxHealth = baseCoral.maxHealth;
        //currentHealth = baseCoral.currentHealth;
        maxHealth = FindObjectOfType<PlayerEvents>().maxHealth;
        currentHealth = FindObjectOfType<PlayerEvents>().currentHealth;
    }

    void Update ()
    {
        currentHealth = FindObjectOfType<PlayerEvents>().currentHealth;
        transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
        if (currentHealth > maxHealth) // Keeps progress in check (basically stops "increasing" after player has met goal
        {
            currentHealth = maxHealth;
        }
    }
}
