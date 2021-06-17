using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{   
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
        move(me);
        checkGround(groundCheck, 0.4f, groundMask); //are we in air?
    }
    
         
    void checkGround(Transform Object, float Radius , LayerMask Layer)
    {
        isGrounded = Physics.CheckSphere(Object.position, Radius, Layer); //returns a bool if a sphere around object, with the radius 'radius' is touching anything with the 'layer'
    }
    void move(Rigidbody Object)
    {
        xDiretionMovement = Input.GetAxis("Horizontal");  // find if the player is trying to move left or right. 
        animator.SetBool("isWalking", false); //we set this to false as a precaution, this way we can simplyfy our logic and only worry about setting it to true
        smooth_turn();
        Object.AddForce(xDiretionMovement*3000f, 0, 0, ForceMode.Force);  //move the player
        
        if(Input.GetKeyDown("space") && isGrounded){Object.AddForce(xDiretionMovement*1500f, 300f, 0, ForceMode.Force);}
    }
    
    void smooth_turn()
    {
        if (xDiretionMovement > 0)
            {
                animator.SetBool("isWalking",true); //start the walking animation
                body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0f, 90f, 0), Time.deltaTime * 5);

            } 
        else if(xDiretionMovement < 0) 
            {
                animator.SetBool("isWalking",true); //start the walking animation
                body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0f, -90f, 0), Time.deltaTime * 5);
            }
    }
}
