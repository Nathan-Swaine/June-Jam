using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    float gravity = -30f;
    
    public Rigidbody rb;
    public Transform Object;
    public LayerMask groundMask; 
 
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    void shoot()
    {
        
        Object.localRotation = Quaternion.Euler(0,0,45);
        rb.AddForce(45f,10,0, ForceMode.Acceleration);
    }
}
