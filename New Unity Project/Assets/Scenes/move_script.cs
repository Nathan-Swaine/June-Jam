using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_script : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Get movement direction
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    }

    void FixedUpdate() 
    {
        controller.move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
