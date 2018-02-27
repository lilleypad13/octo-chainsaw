using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjScript : MonoBehaviour {

    //public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            Destroy(gameObject);
        }
        //if(other.transform != this.transform) // Makes it hit ANYTHING other than itself
        //{
        //    other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver); // Only works with scripts that has TakeDamage function
        //    Destroy(gameObject);
        //}
    }
}
