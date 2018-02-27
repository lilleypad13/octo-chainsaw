using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTRangedEnemy : MonoBehaviour {

    public float attackRange;
    public int damage;
    public float lastAttackTime;
    public float attackDelay;
    public Transform target;
    public float chaseRange;

    public GameObject projectile;
    public float projectileForce;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position); // Checks distance between this object and target's position
        if (distanceToPlayer <= attackRange)
        {
            //Turn towards target
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg; // May need 90 deg offset
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward); //May want forward instead of right
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

            //Check to see if it's time to attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                //Raycast to see if we have line of sight to the target
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange); //May need to change transform up
                //Check to see if we hit anything and what it was
                if (hit.transform == target)
                {
                    //Hit target - fire the projectile
                    GameObject newProj = Instantiate(projectile, transform.position, transform.rotation);
                    newProj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileForce, 0f)); // May want to put force in different direction
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
