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
        
        //Variables 
        public Vector3 fAndBVector3;
        public Vector3 lAndRVector3;
        public void Update()
        {
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
