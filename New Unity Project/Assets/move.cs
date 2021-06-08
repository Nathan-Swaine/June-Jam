using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{   
    public Rigidbody rb;    
    public float speed = 5f;
 

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        
        if ( Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            rb.velocity = new Vector3 (x, 0, 0 ).normalized * speed ;
        }

        
      

         
        
    }
}
