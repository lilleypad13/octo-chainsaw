using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinBasicProjMovement : MonoBehaviour {

    public float speed = 1.0f;


    public Rigidbody2D rb;
    private Quaternion quat;

    private void Awake()
    {
        quat = GetComponent<Quaternion>();
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.FromToRotation(transform.up, quat.eulerAngles);
    }

    void FixedUpdate () {
        rb.velocity = Vector3.up * speed;
	}
}
