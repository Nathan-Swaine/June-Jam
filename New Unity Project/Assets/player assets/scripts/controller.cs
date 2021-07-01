using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{   
    public Transform groundCheck, body, weaponCurvePoint;
    public float groundDistance = 0.4f, xDiretionMovement, playerSpeed; 
    public LayerMask groundMask; 
    public bool isGrounded;
    public Rigidbody me; 
    Animator animator; 
    public Vector3 end;
    public DeathManager deathCall;
    public AudioSource hitSound;

    int playerHealth = 100;
    public HealthBar healthBarObject;

    void Start()
    {
        me = GetComponent<Rigidbody>(); // get the ridigid body from the inspector.
        animator = GetComponent<Animator>();
    }
    void Update() // Update is called once per frame
    {
        move(me);
        checkGround(groundCheck, 0.4f, groundMask); //are we in air?


        if (playerHealth <= 0)
        { 
            deathCall.PlayerDied();
            Destroy(gameObject);
        }
    }
    
    void checkGround(Transform Object, float Radius , LayerMask Layer){isGrounded = Physics.CheckSphere(Object.position, Radius, Layer);}//returns a bool if a sphere around object, with the radius 'radius' is touching anything with the 'layer'}

    void move(Rigidbody Object)
    {
        xDiretionMovement = Input.GetAxis("Horizontal");  // find if the player is trying to move left or right. 
        animator.SetBool("isWalking", false); //we set this to false as a precaution, this way we can simplyfy our logic and only worry about setting it to true
        smooth_turn(body);
        Object.AddForce(xDiretionMovement*playerSpeed, 0, 0, ForceMode.Force);  //move the player
        if(Input.GetKeyDown("space") && isGrounded){Object.AddForce(0, 300f, 0, ForceMode.Acceleration);}
    }
    
    void smooth_turn(Transform target)
    {
        if (xDiretionMovement > 0)
            {
                animator.SetBool("isWalking",true); //start the walking animation
                target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(0f, 90f, 0), Time.deltaTime * 5);
                weaponCurvePoint.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(0f, 90f, 0), Time.deltaTime * 5);

            } 
        else if(xDiretionMovement < 0)
            {
                animator.SetBool("isWalking",true); //start the walking animation
                target.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(0f, -90f, 0), Time.deltaTime * 5);
                weaponCurvePoint.rotation = Quaternion.Lerp(target.rotation, Quaternion.Euler(0f, -90f, 0), Time.deltaTime * 5);
            }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("spear"))
        {
            playerHealth -= 10;
            healthBarObject.SetHealth(playerHealth);
            hitSound.Play();
        }
    }
}
