using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 20f;
   
    public float gravity = -9.81f;
    Vector3 velocity;
    bool ground;
    public Transform groundTransform;
    public LayerMask groundMask;

    // Update is called once per frame
    void Update()
    {
        ground = Physics.CheckSphere(groundTransform.position, 0.4f, groundMask);
      
        if(ground){
            velocity.y = -1f;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);  
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *Time.deltaTime);
    }
}
