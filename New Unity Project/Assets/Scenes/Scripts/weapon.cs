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
    public BoxCollider spearCollider;
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
        spearCollider.GetComponent<Collider>().enabled = true;
        Spear.transform.parent = null;
        Object.isKinematic = false;
        Object.useGravity = true;
        Object.AddForce(100f,force,0, ForceMode.Force);
        isHolding = false;        
    }
}
