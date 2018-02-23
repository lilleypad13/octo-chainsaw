using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseHealth : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;
    private BaseCoral baseCoral;

    // Attach this type script to an object that represents a progress bar/meter of some kind.
    // This will scale that object from 0 - 100% based on the currentHealth variable (how close player is to goal) and the maxHealth value (the actual goal value to be reached).
    // The script being searched for to find the correct variables will need changed, as well as the names of said variables.
    // Could use efficiency updates

    void Start()
    {
        //baseCoral = GetComponent<BaseCoral>();
        //maxHealth = baseCoral.maxHealth;
        //currentHealth = baseCoral.currentHealth;
        maxHealth = FindObjectOfType<BaseCoral>().maxHealth;
        currentHealth = FindObjectOfType<BaseCoral>().currentHealth;
    }

    void Update ()
    {
        currentHealth = FindObjectOfType<BaseCoral>().currentHealth;
        transform.localScale = new Vector3(currentHealth / maxHealth, 1.0f, 1.0f);
        if (currentHealth > maxHealth) // Keeps progress in check (basically stops "increasing" after player has met goal
        {
            currentHealth = maxHealth;
        }
    }
}
