using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    bool isGravity;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        isGravity = true;
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
        }
    }
    void GravityChangeSystem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isGravity = !isGravity;
            if(isGravity)
            {
                rb.gravityScale = 1;
            }
            else if (!isGravity)
            {
                rb.gravityScale = -1;
            }
        }
    }
}
