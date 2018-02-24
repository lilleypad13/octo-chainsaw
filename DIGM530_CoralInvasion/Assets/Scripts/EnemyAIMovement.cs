using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMovement : MonoBehaviour {

    public float moveSpeed = 2.0f;
    public int rotationSpeed = 2;

    private Transform myTransform;
    private Transform target;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
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
        myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
    }

}
