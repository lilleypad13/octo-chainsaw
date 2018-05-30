using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    

    public float speed = 1.0f;
    private float Stop = 0.0f;

    private Rigidbody2D rb;
    private float initialSpeed;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
	}

    private void Update()
    {

        if (Input.GetButton("Fire3") == false)
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.AddForce(movement * speed * Time.deltaTime);
        }
    }
    }
    
