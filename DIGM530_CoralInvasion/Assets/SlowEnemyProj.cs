using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemyProj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Collider2D>();
        //Debug.Log("test");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
 
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        Debug.Log("Test");
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            
            enemy.gameObject.SendMessage("Slow");
            
        }
    }
    private void OnTriggerExit2D(Collider2D enemy)
    {
        Debug.Log("Test2");
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            enemy.gameObject.SendMessage("exit");
        }
    }
}
