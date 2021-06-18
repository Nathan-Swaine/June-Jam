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
    public enum WeaponHas{holding, thrown, flying, returning}; //this shit just crazy
    public WeaponHas weaponState; //SO imagine WeaponHas (line 12) like an array of different values. Except all of these values are of a custom type, we define this type on line 13. Without Line 13, Line 12 would not work because the computer would not know / have a type for these values.


    void Start()
        {
            rb = GetComponent<Rigidbody>(); 
            weaponState = WeaponHas.holding;
            weaponSetup(rb, weaponState, currentWeapon.transform);

            }
    void Update() // Update is called once per frame
        {   
            if (Input.GetButtonDown("Fire1") && weaponState == WeaponHas.holding)
                {
                    
                    fire(rb, throwPower, currentWeapon.transform);
                } 
            if (weaponState == WeaponHas.flying)
                {
                    tipCollider.GetComponent<Collider>().enabled = true;
                }
           
            if (weaponState == WeaponHas.thrown && Input.GetButtonDown("Fire2"))
                {
                    weaponState = WeaponHas.returning;
                }
            if (weaponState == WeaponHas.returning)
                {
                    recall(rb, weaponCurvePoint, hand);
                }
            weaponSetup(rb,weaponState, currentWeapon.transform);
            
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
                    if (weapon.position == hand.position)
                        {
                            weaponState = WeaponHas.holding;
                        }
                    
                }
            else if( time >= 1.00f)
            {
                weaponState = WeaponHas.holding;
            }
        }
    void fire(Rigidbody weapon, float force, Transform weaponTransform)
        {
            weaponState = WeaponHas.flying;
            weaponSetup(weapon, weaponState, weaponTransform);
            getThrowDirection();
            weapon.AddForce(force* throwDirection, force/2, 0, ForceMode.Impulse); //Impluse looks better than acceleration or Force
            
            weaponSpin(weaponTransform); // needs to spin to look good
        
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
            if(weaponState == WeaponHas.flying) // if traveling 
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
                    weaponState = WeaponHas.thrown;
                }
        } 
    void weaponSetup (Rigidbody weapon, WeaponHas weaponState, Transform weaponTransform)
        {
            if (weaponState == WeaponHas.holding)
                {
                    weapon.isKinematic = true;
                    weapon.useGravity = false;
                    currentWeapon.transform.parent = hand;
                    currentWeapon.transform.rotation = hand.rotation;
                    time = 0.0f;
                }

            if (weaponState == WeaponHas.flying)
                {
                    weapon.transform.parent = null; //de-couple from the parent, so we can fly off into the distance 
                    weapon.isKinematic = false; // object is not static
                    weapon.useGravity = true; // object is subjected to gravity
                }

            if (weaponState == WeaponHas.thrown)
                {
                 time = 0.0f;   
                }

            
        }
    
}