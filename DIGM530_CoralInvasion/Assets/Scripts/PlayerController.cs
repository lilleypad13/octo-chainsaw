using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {



    public float speed = 1.0f;

    //private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector3 facingDirection;
    private float initialSpeed;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
        facingDirection = transform.localScale;
        initialSpeed = speed;
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);
        if (moveHorizontal > 0) // Need to fix so if moving in non facingDirection, player returns to idle in that direction. Currently it is always
            // in idle in facingDirection because it is the only state that includes moveHorizontal value of 0.
        {
            transform.localScale = new Vector3(facingDirection.x * -1, facingDirection.y);
        }
        else
        {
            transform.localScale = facingDirection;
        }
    }
}
