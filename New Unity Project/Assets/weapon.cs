using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour{
    public Rigidbody rb;
    public Transform weaponCurvePoint;
    public GameObject currentWeapon, hand;
    public bool isHolding= true , inAir = false;   
    public BoxCollider tipCollider;
    public float throwPower= 100f, throwDirection; 
    
    private Vector3 startPos;
    private Vector3 endPos;

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
                    Debug.Log("Fire");
                    startPos = currentWeapon.transform.position;
                    endPos = hand.transform.position;
                    recall(currentWeapon, startPos ,endPos);
                    
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
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            
            
        }

    void recall(GameObject weapon, Vector3 startPos, Vector3 endPos)
        { 
            float time = 0.0f;
            if (time < 1.0f)
                {
                    Debug.Log("Fire");
                    
                    weapon.transform.position = Vector3.Lerp(startPos, endPos, time);
                    //weapon.position = getBQCPoint(time, oldPos, midpoint.transform.position , target.transform.position);
                    time += Time.deltaTime;
                }
        }

    Vector3 getBQCPoint( float time, Vector3 p0, Vector3 p1, Vector3 p2) //p0 old pos of axe || p1 is point which affects curve / 'curve point' || p2 is the target / players hand / 'hand'
    {
        float u = 1 - time;
        float tt = time * time;
        float uu = u * u; 
        Vector3 p = (uu * p0) + (2 * u * time * p1) + (tt *p2);
        return p;
    }
}