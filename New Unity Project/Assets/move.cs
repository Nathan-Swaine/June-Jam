using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{   
    
    public Rigidbody rb;    
    public float speed = 50f; //player move speed
    public float jumpForce = 400f; //player jump force applied as vector mangnitude
    public float gravity = -30f; 
    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    bool isGrounded;
    void Start()
    {
       rb = GetComponent<Rigidbody>(); // get the ridigid body from the inspector. 
    }

 
    void Update() // Update is called once per frame
    {
        float x = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector3 (x, 0, 0 ).normalized * speed ;
        //these two lines handle horizontal movement across the screen. .normalized is essential here

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
      

        switch (isGrounded)
        {
            case false: //is in air
                gravity -=  8 *Time.deltaTime;
                rb.velocity = new Vector3(x, gravity,0);
            break;

            case true: //is not in air
                gravity = -1f;

            break;
            
        }

        if (Input.GetKey("space"))
            {
            x *= speed;
            //new velocity velocityBefore;
            //velocityBefore = rb.velocity;
            //velocityBefore += 
            //rb.velocity += new Vector3(x, jumpForce,0);

            Vector3 jumpVector; 
            jumpVector = new Vector3(x,2f,0f);

            rb.AddForce(jumpVector, jumpForce*Time.deltaTime, ForceMode.Impulse);
        
            rb.velocity = new Vector3 (x, 0, 0 ).normalized * speed ;
            
            
            }
        
    }
}
