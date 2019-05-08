using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorSystem : MonoBehaviour
{
    public LayerMask WhatIsBlock;
    public string ColorName;

 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorCheck();
    }


    void ColorCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.5f, WhatIsBlock);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.up, 1.5f, WhatIsBlock);

        var checkSystem = hit = hit2;

        if (checkSystem.collider != null)
        {
            
            var checkColor = checkSystem.collider.GetComponent<ChangeGravity>().colorBlock;
            if (ColorName.Equals(checkColor))
            {
                Debug.Log("Is Red");
            }
            
        }



    }
}
