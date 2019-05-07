using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    Transform global;
    [SerializeField]
    Transform posBlocks;
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
    [SerializeField]
    LayerMask whatIsBlock;
    Rigidbody2D rb;
    RaycastHit2D hit;
    

    bool isGrounded;
    bool isHited;
    bool facingLeft;
    bool gravityChange;
    [HideInInspector]
    public bool isGrabing;
    // Start is called before the first frame update
    void Start()
    {
        gravityChange = true;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        facingLeft = false;
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
            GrabBox();
            Debug.Log(isGrabing);
        }
    }

    void Walk()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        InputMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(InputMove * speed, rb.velocity.y);
       
            if (InputMove > 0 && facingLeft)
            {
                 if (!isGrabing)
                 {
                    Flip();
                 }
            }
            else if (InputMove < 0 && !facingLeft)
            {
                 if (!isGrabing)
                 {
                    Flip();
                 }
             }
        
        
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
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

    void GrabBox()
    {
        if (!facingLeft)
        {
           
            isHited = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, whatIsBlock);
            hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, whatIsBlock);
        }
        else if (facingLeft)
        {
            isHited = Physics2D.Raycast(transform.position, -Vector2.right, 1.5f, whatIsBlock);
            hit = Physics2D.Raycast(transform.position, -Vector2.right, 1.5f, whatIsBlock);
        }
       
        if (isHited)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrabing = !isGrabing;

                if (hit.collider != null)
                {
                    var col = hit.collider.GetComponent<Rigidbody2D>();
                    if (isGrabing)
                    {
                        Debug.Log("Catch");
                        hit.collider.transform.parent = posBlocks.transform;
                        col.isKinematic = true;
                    }
                    else if (!isGrabing)
                    {
                        hit.collider.transform.parent = global.transform;
                        col.isKinematic = false;
                    }

                }

            }
        }
        
    }
    
    

}
