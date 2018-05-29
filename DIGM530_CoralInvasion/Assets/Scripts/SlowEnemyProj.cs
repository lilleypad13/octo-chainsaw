using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pathfinding
{
    using Pathfinding.Util;
    public class SlowEnemyProj : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            this.GetComponent<Collider2D>();
            //Debug.Log("test");
        }

        // Update is called once per frame
        void Update()
        {

        }

       /* private void OnTriggerEnter2D(Collider2D enemy)
        {
            Debug.Log("I just touched " + enemy.name);
            if (enemy.transform.parent.GetComponent<AILerp>())
            {
                enemy.transform.parent.GetComponent<AILerp>().Slow();
                Debug.Log("Slow message has been sent to enemy.");
            }
            //enemy.gameObject.SendMessage("Slow");
        }
        private void OnTriggerExit2D(Collider2D enemy)
        {
            Debug.Log("I just stopped touching " + enemy.name);
            if (enemy.transform.parent.GetComponent<AILerp>())
            {
                enemy.transform.parent.GetComponent<AILerp>().Exit();
                Debug.Log("Exit message has been sent to enemy.");
            }
        }*/
         private void OnTriggerEnter2D(Collider2D enemy)
         {
             //Debug.Log("Test");
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
                 enemy.gameObject.SendMessage("Exit");
             }
         }
    }
}
