using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    [SerializeField]
    LayerMask WhatIsPlayer;
    Rigidbody2D rb;
    GameObject player;
    PlayerControl playerControl;
    bool isGravity;
    bool hasPlayer;

    // Start is called before the first frame update
    void Start()
    {
        isGravity = true;
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
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
            GravityChangeSystem();
            PlayerProximity();
            
            
        }
    }
    void GravityChangeSystem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isGravity = !isGravity;
            if(isGravity)
            {
                Debug.Log(rb.gravityScale);
                rb.gravityScale = 1;
            }
            else if (!isGravity)
            {
                rb.gravityScale = -1;
            }
        }
    }
    void PlayerProximity()
    {
        hasPlayer = Physics2D.OverlapCircle(transform.position, 0.5f, WhatIsPlayer);
        if(playerControl.isGrabing == false)
        {
            if (hasPlayer)
            {
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = false;
            }
        }
        
    }

}
