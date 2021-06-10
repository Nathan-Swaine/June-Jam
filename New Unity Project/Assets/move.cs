using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{   
    
    public Rigidbody rb;    
    public float speed = 5f; //player move speed
    public float jumpForce = 15f; //player jump force applied as vector mangnitude
    public float gravity;
    


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

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3 (x, 0, 0 ).normalized * speed ;
        //these two lines handle horizontal movement across the screen. .normalized is essential here

        if (Input.GetButtonDown("jump")) 
        {   //add a jump force whenever buttons associated with jump are pressed, look in edit>project-settings>input manager.
            rb.velocity = new Vector3(x, jumpForce, 0) *speed; 
        }
     

        //gravity += -4.95f + (gravity/50);
        //rb.velocity = new Vector3(x, gravity,0);

        rb.velocity = new Vector3(x, gravity,0); 

        
        gravity += -4.95f *Time.deltaTime;
       
       
       
       
        //adds gravity force to the model, deltaY = 1/2g *(Time)^2 => deltaY = -4.95*(Time*Time) => deltay = -4.95*(fraction of a second) 
        //==> there fore because its fractions of a second eg 0.1 we need to flip the multiply sign into a divide. 
        // because this runs each frame acceleration due to gravity is massive, needs to be slowed down. 

         
        
    }
}
