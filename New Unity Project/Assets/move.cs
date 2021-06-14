using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{   
    float gravity = -30f;
    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    public bool isGrounded;
    public Rigidbody me;  
    //public Rigidbody selectedWeapon;
    public float xDiretionMovement;
   

    void Start()
    {
        me = GetComponent<Rigidbody>(); // get the ridigid body from the inspector.
       
    }

    void Update() // Update is called once per frame
    {

        xDiretionMovement = Input.GetAxis("Horizontal");  // move the player left or right, if they are not moving then its 50 * 0, so they wont move
        me.AddForce(xDiretionMovement*3000f, 0, 0, ForceMode.Force);  //these two lines handle horizontal movement across the screen.
        //selectedWeapon.AddForce(xDiretionMovement,0,0,ForceMode.Force);
        checkGround(groundCheck, 0.4f, groundMask); //are we in air?
        checkGravity(); //check if we are jumping / on ground
        
        if (xDiretionMovement > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }else if(xDiretionMovement < 0 )
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);

        }
    }

    void checkGravity()
    {
    
        if(!isGrounded)//in air
        {
            gravity -= 8*Time.deltaTime;
            me.AddForce(xDiretionMovement, gravity, 0, ForceMode.Acceleration);
            //selectedWeapon.AddForce(xDiretionMovement,gravity,0,ForceMode.Acceleration);

        }
        else// on ground
        {
            gravity = -1f;// sometimes the checkground will return true despite been slightly above the ground, this will 'suck' the player down slightly. 
        }
        if (Input.GetKeyDown("space") && isGrounded) //on ground and pressing space
            {
                me.AddForce(0, 100f, 0, ForceMode.Impulse);
                me.AddForce(xDiretionMovement*1500f, 0, 0, ForceMode.Force);
                //selectedWeapon.AddForce(xDiretionMovement,200f,0,ForceMode.Impulse);

            }
    }       

    void checkGround(Transform Object, float Radius , LayerMask Layer)
    {
        isGrounded = Physics.CheckSphere(Object.position, Radius, Layer); //returns a bool if a sphere around object, with the radius 'radius' is touching anything with the 'layer'
        checkGravity();
    }
}
