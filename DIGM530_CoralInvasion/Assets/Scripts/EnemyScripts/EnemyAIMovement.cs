using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMovement : MonoBehaviour {

    public float initialMoveSpeed = 2.0f;
    public float currentMoveSpeed;
    public int rotationSpeed = 2;

    private Transform myTransform;
    private Transform target;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        currentMoveSpeed = initialMoveSpeed;
        GameObject go = GameObject.FindGameObjectWithTag("Base");
        target = go.transform;
    }

    void FixedUpdate()
    {
        Vector3 dir = target.position - myTransform.position;
        //dir.z = 0.0f; // Only needed if objects don't share 'z' value
        if (dir != Vector3.zero)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.FromToRotation(Vector3.right, dir),
                rotationSpeed * Time.deltaTime);
        }

        //Move Towards Target
        myTransform.position += (target.position - myTransform.position).normalized * currentMoveSpeed * Time.deltaTime;
    }

    //private void Update()
    //{
    //    // These are the proper arrangements to translate the raycast setup from the example to our enemies
    //    //Debug.DrawRay(transform.position - (transform.right / 8), -transform.up * 20, Color.yellow);
    //    //Debug.DrawRay(transform.position - (transform.right / 8), transform.up * 20, Color.yellow);
    //    //Debug.DrawRay(transform.position + (transform.up / 8), transform.right * 20, Color.red);
    //    //Debug.DrawRay(transform.position - (transform.up / 8), transform.right * 20, Color.red);
    //}

}
