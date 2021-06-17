using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Rigidbody rb;
    public Transform currentWeapon;
    public bool isHolding= true;   
    public bool inAir = false; 
    public BoxCollider tipCollider;
    public float throwPower= 100f; 
    public float throwDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        rb.isKinematic = true;
        rb.useGravity = false;
        isHolding = true;
    }
    void Update() // Update is called once per frame
    {   
        if (Input.GetButtonDown("Fire1")){fire(rb, throwPower, currentWeapon);} 
        if (inAir){tipCollider.GetComponent<Collider>().enabled = true;}
            
    }
    void fire(Rigidbody Object, float force, Transform weapon)
    {
        if (isHolding)
        {
            throwDirection = Input.GetAxis("Horizontal");
            if (throwDirection > 0 && throwDirection < 1) {throwDirection = 1;} 
            else if(throwDirection < 0 && throwDirection > -1){throwDirection =-1;}//this if statement and the one above are so that we can still throw the weapon in the direction the player is facing without diluting the throw power because of fractional scaling.
            weapon.transform.parent = null; //de-couple from the parent, so we can fly off into the distance 
            Object.isKinematic = false; // object is not static
            Object.useGravity = true; // object is subjected to gravity
            Object.AddForce(force *throwDirection, force /2, 0, ForceMode.Impulse); //Impluse looks better than acceleration or Force
            
            isHolding = false;//we are not holding the weapon
            inAir = true; // because its in the air
            weaponSpin(weapon); // needs to spin to look good
        }
    }

    void weaponSpin(Transform weapon)
    {
        if(!isHolding && inAir){weapon.localEulerAngles += Vector3.forward * 500 / Time.deltaTime; //change vector 3 forward to something else , maybe vector right left depending on xdirection input? }
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.name == "Player"){tipCollider.GetComponent<Collider>().enabled = false;}
    }
}}
