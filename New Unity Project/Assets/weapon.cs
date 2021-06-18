using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour{
    public Rigidbody rb;
    public Transform weaponCurvePoint, hand;
    public GameObject currentWeapon;
    public BoxCollider tipCollider;
    public float throwPower= 100f, throwDirection, time = 0.0f, lastFacing; 
    public Vector3 oldPos, newPos;
    public enum weaponState{holding, thrown, flying, returning};
    public weaponState weaponstate;


    void Start()
        {
            rb = GetComponent<Rigidbody>(); 
            rb.isKinematic = true;
            rb.useGravity = false;
            weaponstate = weaponState.holding;
            }
    void Update() // Update is called once per frame
        {   
            if (Input.GetButtonDown("Fire1"))//a    && weaponstate == weaponState.holding)
                {
                    fire(rb, throwPower, currentWeapon.transform);
                } 
            if (weaponstate == weaponState.flying)
                {
                    tipCollider.GetComponent<Collider>().enabled = true;
                }
           
            if (weaponstate == weaponState.thrown && Input.GetButtonDown("Fire2"))
            {
                weaponstate = weaponState.returning;
            }
            if (weaponstate == weaponState.returning)
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
                    weaponstate = weaponState.holding;
                }
            else if( time >= 1.00f)
            {
                weaponstate = weaponState.holding;
            }
        }
    void fire(Rigidbody Object, float force, Transform weapon)
        {
         
            weapon.transform.parent = null; //de-couple from the parent, so we can fly off into the distance 
            Object.isKinematic = false; // object is not static
            Object.useGravity = true; // object is subjected to gravity
            getThrowDirection();
            Object.AddForce(force* throwDirection, force/2, 0, ForceMode.Impulse); //Impluse looks better than acceleration or Force
            
            weaponstate = weaponState.flying;
            weaponSpin(weapon); // needs to spin to look good
        
        }

    void getThrowDirection()
        {
            throwDirection = Input.GetAxis("Horizontal");
            if (throwDirection > 0 && throwDirection < 1) //this if statement and the one above are so that we can still throw the weapon in the direction the player is facing without diluting the throw power because of fractional scaling.
                {
                    throwDirection = 1;
                    lastFacing = throwDirection;
                } 
            else if(throwDirection < 0 && throwDirection > -1) 
                {
                    throwDirection =-1;
                    lastFacing = throwDirection;
                }
        }
    void weaponSpin(Transform weapon)
        {
            if(weaponstate == weaponState.flying) // if traveling 
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
                    weaponstate = weaponState.thrown;
                }
            
            
        }   
}