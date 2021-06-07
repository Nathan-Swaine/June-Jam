using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_script : MonoBehaviour
{

    public charater_controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Input.GetAxisRaw("Horizontal");

    }

    void FixedUpdate() 
    {
    
    }
}
