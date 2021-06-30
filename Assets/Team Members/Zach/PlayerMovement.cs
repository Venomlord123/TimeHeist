using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ZachFrench
{
    public class PlayerMovement : MonoBehaviour
    {
        //References 
        public Rigidbody rigidbody;
        public Transform player;
        public Camera camera;
        //public CharacterController characterController;
        //Variables 
        public Vector3 fAndBVector3;
        public Vector3 lAndRVector3;
        public Vector3 movement;
        public float xAxis;
        public float zAxis;
        public void Update()
        {
            Movement();
        }

        public void Movement()
        {
            /*xAxis = Input.GetAxis("Horizontal");
            zAxis = Input.GetAxis("Vertical");

            movement = transform.right * xAxis + transform.forward * zAxis;

            characterController.SimpleMove(movement);*/
            if (Keyboard.current.wKey.isPressed)
            {
                rigidbody.AddRelativeForce(fAndBVector3);
            }
            if (Keyboard.current.sKey.isPressed)
            {
                rigidbody.AddRelativeForce(-fAndBVector3);
            }
            if (Keyboard.current.aKey.isPressed)
            {
                rigidbody.AddRelativeForce(lAndRVector3);
            }
            if (Keyboard.current.dKey.isPressed)
            {
                rigidbody.AddRelativeForce(-lAndRVector3);
            }
        }
    }
}
