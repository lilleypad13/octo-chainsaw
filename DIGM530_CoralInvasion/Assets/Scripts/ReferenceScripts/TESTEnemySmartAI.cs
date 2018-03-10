using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTEnemySmartAI : MonoBehaviour {

    // Fix a range how early u want your enemy detect the obstacle.
    private int range;
    public float initialMoveSpeed = 2.0f;
    public float currentMoveSpeed;
    //private float speed;
    private bool isThereAnyThing = false;
    public int rotationSpeed = 2;

    // Specify the target for the enemy.
    //public GameObject target;
    private RaycastHit hit;
    private Transform target;
    private Transform myTransform;

    private int layerMask;


    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        range = 2;
        //speed = 10f;
        currentMoveSpeed = initialMoveSpeed;
        //rotationSpeed = 15f;
        GameObject go = GameObject.FindGameObjectWithTag("Base");
        target = go.transform;
        layerMask = 1 << 11;
    }

    void Update()
    {
        //Look At Somthly Towards the Target if there is nothing in front.
        if (!isThereAnyThing)
        {
            Vector3 relativePos = target.position - myTransform.position;
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.FromToRotation(Vector3.right, relativePos),
                Time.deltaTime * rotationSpeed);
        }

        // Enemy translate in forward direction.

        //transform.Translate(Vector3.forward * Time.deltaTime * currentMoveSpeed);
        myTransform.position += (target.position - transform.position).normalized * currentMoveSpeed * Time.deltaTime;

        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;
        //Use Phyics.RayCast to detect the obstacle

        // Attempting to make physics2D version of raycast logic seen below
        RaycastHit2D leftHit = Physics2D.Raycast(leftRay.position + (transform.up / 8), transform.right, range, layerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRay.position - (transform.up / 8), transform.right, range, layerMask);

        //if (leftHit.collider.gameObject.CompareTag("Obstacles") || rightHit.collider.gameObject.CompareTag("Obstacles"))
        //{
        //    isThereAnyThing = true;
        //    transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        //    Debug.Log("Obstacle Seen");
        //}

        if (leftHit.transform.tag == "Obstacles" || rightHit.transform.tag == "Obstacles")
        {
            isThereAnyThing = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            Debug.Log("Obstacle Seen");
        }

        //// Rays Fixed
        //if (Physics.Raycast(leftRay.position + (transform.up / 8), transform.right, out hit, range) ||
        //    Physics.Raycast(rightRay.position - (transform.up / 8), transform.right, out hit, range))
        //    // Rays Fixed
        //{
        //    if (hit.collider.gameObject.CompareTag("Obstacles"))
        //    {
        //        isThereAnyThing = true;
        //        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        //        Debug.Log("Obstacle Seen");
        //    }
        //}
        //// Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        //// Just making this boolean variable false it means there is nothing in front of object.

        // Attempting to make physics2D version of raycast logic seen below
        RaycastHit2D upHit = Physics2D.Raycast(transform.position - (transform.right / 8), transform.up, range, layerMask);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position - (transform.right / 8), -transform.up, range, layerMask);

        //if (upHit.collider.gameObject.CompareTag("Obstacles") || downHit.collider.gameObject.CompareTag("Obstacles"))
        //{
        //    isThereAnyThing = false;
        //}

        if (upHit.transform.tag == "Obstacles" || downHit.transform.tag == "Obstacles")
        {
            isThereAnyThing = false;
        }

        //// Rays Fixed
        //if (Physics.Raycast(transform.position - (transform.right / 8), transform.up, out hit, 1) ||
        // Physics.Raycast(transform.position - (transform.right / 8), -transform.up, out hit, 1))
        // // Rays Fixed
        //{
        //    if (hit.collider.gameObject.CompareTag("Obstacles"))
        //    {
        //        isThereAnyThing = false;
        //    }
        //}
        // Use to debug the Physics.RayCast.
        //Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 20, Color.red);
        //Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 20, Color.red);
        //Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 20, Color.yellow);
        //Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 20, Color.yellow);

        // These are the actual rays
        Debug.DrawRay(transform.position - (transform.right / 8), -transform.up * 2, Color.yellow);
        Debug.DrawRay(transform.position - (transform.right / 8), transform.up * 2, Color.yellow);
        Debug.DrawRay(transform.position + (transform.up / 8), transform.right * 2, Color.red);
        Debug.DrawRay(transform.position - (transform.up / 8), transform.right * 2, Color.red);
    }
}
