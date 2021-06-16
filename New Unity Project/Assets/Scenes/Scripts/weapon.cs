using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Rigidbody rb;
    public Transform spearBottom;
    public Transform spearTip;
    public LayerMask groundMask;
    public Transform Spear;
    public bool isHolding;   
    public bool inAir; 
    public BoxCollider spearCollider;
    public float xDiretionMovement; 
    void Start()
    {
        spearCollider.GetComponent<Collider>().enabled = false;
        rb = GetComponent<Rigidbody>(); 
        rb.isKinematic = true;
        rb.useGravity = false;
        isHolding = true;
    }
    void Update() // Update is called once per frame
    {   
      if (Input.GetButtonDown("Fire1"))
        {
            fire(rb, 500);
        }    
    }
    void fire(Rigidbody Object, float force)
    {
        
        if (isHolding)
        {
            
            xDiretionMovement = Input.GetAxis("Horizontal");  // find if the player is trying to move left or right. 
            Spear.transform.parent = null;
            Object.isKinematic = false;
            Object.useGravity = true;
            Object.AddForce( xDiretionMovement * 500f,force,0, ForceMode.Force);
            isHolding = false;        
            inAir = true;
            weaponSpin();
        }
    }

    void weaponSpin()
    {
        if(!isHolding && inAir)
        {
            Spear.localEulerAngles += Vector3.forward * 5000 * Time.deltaTime; //change vector 3 forward to something else , maybe vector right left depending on xdirection input? 
        }
    }

    
}
