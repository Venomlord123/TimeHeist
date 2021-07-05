using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ZachFrench
{
    public class PlayerModel : MonoBehaviour
    {
        //References 
        public Rigidbody rigidbody;
        //Variables 
        public Vector3 fAndBVector3;
        public Vector3 lAndRVector3;
        public float velocity;
        public float maxSpeed;
        public void Update()
        {
            Movement();
            //Getting Velocity for NPC Movement
            velocity = rigidbody.velocity.magnitude;

            if (rigidbody.velocity.magnitude >= maxSpeed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
            }
        }

        public void Movement()
        {
            //TODO balance movement so that we slide only a little and stop within a couple of seconds
            
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
