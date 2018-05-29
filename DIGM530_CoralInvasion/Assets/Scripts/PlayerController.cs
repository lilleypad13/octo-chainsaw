using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    

    public float speed = 1.0f;

    //private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float initialSpeed;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
        initialSpeed = speed;
	}

    private void FixedUpdate()
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
    
