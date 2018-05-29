using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubAI : MonoBehaviour {

    
    public float initialMoveSpeed = 2.0f;
    public float currentMoveSpeed;
    public float distanceFromBoundary = 2.0f; // Object's distance to boundary before it turns around

    public LayerMask boundaryLayer; // Decides layer of object that will turn this object around when it comes into range of the raycast

    private float lastAttackTime;
    public float attackDelay = 1.0f;
    public GameObject projectile;
    public float projectileForce = 100f;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    private void Start()
    {
        currentMoveSpeed = initialMoveSpeed;
        lastAttackTime = 0f;
    }

    private void FixedUpdate()
    {
        myTransform.position -= transform.right * currentMoveSpeed * Time.deltaTime;
    }

    void Update ()
    {
        TurnAround();
        DropBomb();

        Debug.DrawRay(transform.position, -transform.right * distanceFromBoundary, Color.red);
    }

    // Want to flip gameObject so that it will start moving the opposite direction
    void TurnAround()
    {
        if (Physics2D.Raycast(transform.position, -transform.right, distanceFromBoundary, boundaryLayer))
        {
            transform.Rotate(Vector3.up, 180f);
        }
    }

    void DropBomb()
    {
        if (Time.time > lastAttackTime + attackDelay)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            //newProj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, -projectileForce)); // Throws projectile downward at force equal to projectileForce
            lastAttackTime = Time.time;
        }
    }
}
