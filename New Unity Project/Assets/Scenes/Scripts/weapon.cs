using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Rigidbody rb;
    public Transform spearBottom;
    public Transform spearTip;
    public controller controller;
    public LayerMask groundMask;
    public Transform Spear;
    public bool isGrounded;
    public bool isHolding;
    public float force = 30f;
    public float gravity = -30f; 
    void Start()
    {
        
        rb = GetComponent<Rigidbody>(); 
        isHolding = true;
    }

    // Update is called once per frame
    void Update()
    {   
      if (Input.GetButtonDown("Fire1"))
        {
            fire(rb, force);
        }    
    }

    void checkGravity(Rigidbody Object, float gravity)
    {
        if(!isGrounded)//in air
        {
            gravity -= 8*Time.deltaTime;
            Object.AddForce(0, gravity, 0, ForceMode.Acceleration); //selectedWeapon.AddForce(xDiretionMovement,gravity,0,ForceMode.Acceleration);
        }
        else// on ground
        {
            gravity = -1f;// sometimes the checkground will return true despite been slightly above the ground, this will 'suck' the player down slightly. 
        }
        if (Input.GetKeyDown("space") && isGrounded) //on ground and pressing space
            {
                Object.AddForce(0, 300f, 0, ForceMode.Impulse);
                Object.AddForce(0, 0, 0, ForceMode.Force);//selectedWeapon.AddForce(xDiretionMovement,200f,0,ForceMode.Impulse);
            }
    }       
    void checkGround(Transform Object, float Radius , LayerMask Layer)
    {
        isGrounded = Physics.CheckSphere(Object.position, Radius, Layer); //returns a bool if a sphere around object, with the radius 'radius' is touching anything with the 'layer'
    }

    void fire(Rigidbody Object, float force)
    {
        checkGround(spearTip, 3f, groundMask);
        checkGravity(rb , gravity);
        fire(rb, force);
        isHolding = false;
        rb.isKinematic = false;
        Spear.transform.parent = null;
        Object.AddForce(transform.forward *force, ForceMode.Force);
    }
    
}
