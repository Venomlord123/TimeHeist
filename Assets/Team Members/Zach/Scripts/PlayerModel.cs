using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ZachFrench
{
    public class PlayerModel : MonoBehaviour
    {
        //References 
        public CharacterController characterController;
        //Variables for movement
        [Tooltip("Just a visual of the velocity")]
        public float velocity;
        private float x;
        private float z;
        [Tooltip("Use this to edit how fast you want the player to move")]
        public float speed;
        private Vector3 move;
        //Variables for interact
        public NPCBase tempNpcBase;
        public PlayerJournal playerJournal;
        public Ray ray;
        public RaycastHit hitInfo;
        
        public void Update()
        {
            CharacterMovement();
            //Getting Velocity for NPC Movement
            velocity = characterController.velocity.magnitude;
            
            //raycast for interacting
            InteractionRay();
        }

        private void InteractionRay()
        {
            ray = new Ray(transform.position, transform.forward);
            hitInfo = new RaycastHit();
            Physics.Raycast(ray, out hitInfo);
            if (Mouse.current.leftButton.isPressed)
            {
                if (hitInfo.collider.GetComponent<NPCBase>())
                {
                    tempNpcBase = hitInfo.collider.GetComponent<NPCBase>();
                    playerJournal.GatheredInformation(tempNpcBase);
                }
            }
        }

        public void CharacterMovement()
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            move = transform.right * x + transform.forward * z;

            characterController.Move(move * speed * Time.deltaTime);
        }

    }
}
