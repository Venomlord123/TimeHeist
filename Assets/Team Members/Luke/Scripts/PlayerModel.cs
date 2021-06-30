using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Luke
{
    public class PlayerModel : MonoBehaviour
    {
        //references

        //Variables
        public float movementSpeed;
        public float currentSpeed;
        public Vector3 fAndBVector3;
        public Vector3 lAndRVector3;
        public Rigidbody rb;

        //public CharacterController characterController;
        
        //Events
        
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            /*if (Keyboard.current.wKey.isPressed)
            {
                rb.AddRelativeForce(fAndBVector3);
            }
            if (Keyboard.current.sKey.isPressed)
            {
                rb.AddRelativeForce(-fAndBVector3);
            }
            if (Keyboard.current.aKey.isPressed)
            {
                rb.AddRelativeForce(lAndRVector3);
            }
            if (Keyboard.current.dKey.isPressed)
            {
                rb.AddRelativeForce(-lAndRVector3);
            }*/
        }

        public void PlayerMovementForce(Vector2 direction)
        {
            //characterController.Move(direction * movementSpeed * Time.deltaTime);
            //rb.AddRelativeForce(direction * movementSpeed * Time.deltaTime);
        }
    }
}
