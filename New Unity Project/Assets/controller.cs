using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{   
    float gravity = -30f;
    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask; 
    public bool isGrounded;
    public Rigidbody me; 
     
    public Transform body;
    Animator animator; 
    public float xDiretionMovement;
    public Vector3 end;   

    void Start()
    {
        me = GetComponent<Rigidbody>(); // get the ridigid body from the inspector.
        animator = GetComponent<Animator>();
    }
    void Update() // Update is called once per frame
    {
        move();
        checkGround(groundCheck, 0.4f, groundMask); //are we in air?
        checkGravity(me, gravity); //check if we are jumping / on ground
    }
    void checkGravity(Rigidbody Object, float gravity)
    {
        if(!isGrounded)//in air
        {
            gravity -= 8*Time.deltaTime;
            Object.AddForce(xDiretionMovement, gravity, 0, ForceMode.Acceleration); //selectedWeapon.AddForce(xDiretionMovement,gravity,0,ForceMode.Acceleration);
        }
        else// on ground
        {
            gravity = -1f;// sometimes the checkground will return true despite been slightly above the ground, this will 'suck' the player down slightly. 
        }
        if (Input.GetKeyDown("space") && isGrounded) //on ground and pressing space
            {
                Object.AddForce(0, 300f, 0, ForceMode.Impulse);
                Object.AddForce(xDiretionMovement*1500f, 0, 0, ForceMode.Force);//selectedWeapon.AddForce(xDiretionMovement,200f,0,ForceMode.Impulse);
            }
    }       
    void checkGround(Transform Object, float Radius , LayerMask Layer)
    {
        isGrounded = Physics.CheckSphere(Object.position, Radius, Layer); //returns a bool if a sphere around object, with the radius 'radius' is touching anything with the 'layer'
    }
    void move()
    {
        xDiretionMovement = Input.GetAxis("Horizontal");  // find if the player is trying to move left or right. 
        animator.SetBool("isWalking", false); //we set this to false as a precaution, this way we can simplyfy our logic and only worry about setting it to true
        smooth_turn();
        me.AddForce(xDiretionMovement*3000f, 0, 0, ForceMode.Force);  //move the player

    }
    void smooth_turn()
    {
        if (xDiretionMovement > 0)
        {
            animator.SetBool("isWalking",true); //start the walking animation
            body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0f, 90f, 0), Time.deltaTime * 5);

        } else if(xDiretionMovement < 0) 
        {
            animator.SetBool("isWalking",true); //start the walking animation
            body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0f, -90f, 0), Time.deltaTime * 5);
        }
    }
}
