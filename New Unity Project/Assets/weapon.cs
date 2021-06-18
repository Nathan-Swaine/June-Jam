using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour{
    public Rigidbody rb;
    public Transform weaponCurvePoint, hand;
    public GameObject currentWeapon;
    public bool isHolding= true , inAir = false, isReturning= false;   
    public BoxCollider tipCollider;
    public float throwPower= 100f, throwDirection, time = 0.0f; 
    public Vector3 oldPos, newPos;


    void Start()
        {
            rb = GetComponent<Rigidbody>(); 
            rb.isKinematic = true;
            rb.useGravity = false;
            isHolding = true;
        }
    void Update() // Update is called once per frame
        {   
            if (Input.GetButtonDown("Fire1"))
                {
                    fire(rb, throwPower, currentWeapon.transform);
                } 
            if (inAir)
                {
                    tipCollider.GetComponent<Collider>().enabled = true;
                }
           
            if (!isHolding && !inAir && Input.GetButtonDown("Fire2"))
            {
                isReturning = true;
            }
            if (isReturning)
            {
                recall(rb, weaponCurvePoint, hand);
            }
        }
    void recall(Rigidbody weapon, Transform midpoint, Transform hand)
        { 
            if (time < 1.0f)
                { 
                    oldPos = rb.position;
                    weapon.velocity = Vector3.zero;
                    weapon.isKinematic = true;
                    
                    time += Time.deltaTime;
                    Vector3 newPos= Vector3.Lerp(oldPos, hand.position, time);
                    weapon.position = newPos;
                    
                    
                    
                    

                }
            else if( time >= 1.00f)
            {
                isReturning = false;
            }
        }
    void fire(Rigidbody Object, float force, Transform weapon)
        {
            if (isHolding)
            {
                throwDirection = Input.GetAxis("Horizontal");
                    
                if (throwDirection > 0 && throwDirection < 1) //this if statement and the one above are so that we can still throw the weapon in the direction the player is facing without diluting the throw power because of fractional scaling.
                    {
                        throwDirection = 1;
                    } 
                else if(throwDirection < 0 && throwDirection > -1) 
                    {
                        throwDirection =-1;
                    }
                weapon.transform.parent = null; //de-couple from the parent, so we can fly off into the distance 
                Object.isKinematic = false; // object is not static
                Object.useGravity = true; // object is subjected to gravity
                Object.AddForce(force *throwDirection, force/2, 0, ForceMode.Impulse); //Impluse looks better than acceleration or Force
                
                isHolding = false;//we are not holding the weapon
                inAir = true; // because its in the air
                weaponSpin(weapon); // needs to spin to look good
            }
        }

    void weaponSpin(Transform weapon)
        {
            if(!isHolding && inAir) // if traveling 
                {
                    weapon.transform.RotateAround(weapon.transform.position, Vector3.back, 2000* Time.deltaTime);
                }
        }

    void OnCollisionEnter(Collision col) 
        {
            if (col.gameObject.name == "Player")
                {
                    tipCollider.GetComponent<Collider>().enabled = false;
                }
            else
                {                    
                    inAir = false;
                }
            
            
        }   
}