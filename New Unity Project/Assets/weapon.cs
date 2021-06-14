using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    public Rigidbody rb; 
    public Transform Object;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
       Object = GetComponent<Transform>();
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
        rb.AddForce(45f,0,0, ForceMode.Acceleration);
    }
}
