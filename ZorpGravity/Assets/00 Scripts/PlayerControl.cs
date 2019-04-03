﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundRadius;
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    int extraJumpsValue;
    int extraJumps;
    float InputMove;


    [SerializeField]
    LayerMask whatIsGround;
    Rigidbody2D rb;

    bool isGrounded;
    bool facingRight;
    bool gravityChange;

    // Start is called before the first frame update
    void Start()
    {
        gravityChange = true;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Action");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Action()
    {
        while (true)
        {
            yield return null;
            Walk();
            GravitySystem();
        }
    }

    void Walk()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        InputMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(InputMove * speed, rb.velocity.y);
        if(InputMove > 0 && facingRight == true)
        {
            Flip();
        }
        else if (InputMove < 0 && facingRight == false)
        {
            Flip();
        }
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    
    void GravitySystem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gravityChange = !gravityChange;
            if(gravityChange)
            {
                rb.gravityScale = 1;
            }
            else if (!gravityChange)
            {
                rb.gravityScale = -1;
            }
        }
    }
}
