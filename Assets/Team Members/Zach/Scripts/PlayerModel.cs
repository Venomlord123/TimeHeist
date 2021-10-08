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
        [HideInInspector] 
        public Vector3 velocityNorm;
        //Variables for interact
        public NPCBase tempNpcBase;
        public PlayerJournal playerJournal;
        public Ray ray;
        public RaycastHit hitInfo;
        public Vector3 startPosition;
        public Quaternion startRotation;
        public GameManager gameManager;


        private void OnEnable()
        {
            gameManager.JournalSwitchSceneEvent += PlayerResetTransform;
        }
        
        private void OnDisable()
        {
            gameManager.JournalSwitchSceneEvent -= PlayerResetTransform;
        }

        private void PlayerResetTransform()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }

        public void Start()
        {
            //todo add to TDD for reference to layer
            Physics.IgnoreLayerCollision(6,7);
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        public void Update()
        {
            CharacterMovement();
            //Getting Velocity for NPC Movement
            velocity = characterController.velocity.magnitude;
            velocityNorm = characterController.velocity.normalized;
            
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
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.GetComponent<NPCBase>())
                    {
                        tempNpcBase = hitInfo.collider.GetComponent<NPCBase>();
                        if (playerJournal is { }) playerJournal.GatheredInformation(tempNpcBase);
                    }
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
