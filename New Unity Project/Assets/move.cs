using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{   
    float speed = 50f;
    float gravity = -30f;
    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    bool isGrounded;
    public Rigidbody rb;    
    
    void Start()
    {
       rb = GetComponent<Rigidbody>(); // get the ridigid body from the inspector. 
    }

    void Update() // Update is called once per frame
    {
        float x = Input.GetAxis("Horizontal") * speed;
        rb.AddForce(x, 0, 0, ForceMode.Force);  //these two lines handle horizontal movement across the screen. .normalized is essential here

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    
        if(!isGrounded)
        {
            gravity -= 8*Time.deltaTime;
            rb.AddForce(x, gravity, 0, ForceMode.Acceleration);
        }
        else
        {
            gravity = -1f;
        }
        if (Input.GetKeyDown("space") && isGrounded)
            {
                rb.AddForce(x, 200f,0, ForceMode.Impulse);    
            }
        
    }
}
