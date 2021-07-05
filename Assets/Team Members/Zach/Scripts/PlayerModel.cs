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
        public CharacterController characterController;
        //Variables 
        [Tooltip("Just a visual of the velocity")]
        public float velocity;
        private float x;
        private float z;
        [Tooltip("Use this to edit how fast you want the player to move")]
        public float speed;
        private Vector3 move;
        public void Update()
        {
            CharacterMovement();
            //Getting Velocity for NPC Movement
            velocity = characterController.velocity.magnitude;
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
