using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Luke
{
    public class PlayerController : MonoBehaviour
    {
        //references
        public PlayerInput playerInput;
        public PlayerModel playerModel;
        
        //Variables
        public Vector2 currentDirection;

        public void Start()
        {
            playerInput = new PlayerInput();
            playerInput.Enable();

            playerInput.Main.Movement.performed += PlayerMovementInput;
            playerInput.Main.Interact.performed += PlayerInteractInput;
        }

        public void Update()
        {
            
        }

        public void PlayerMovementInput(InputAction.CallbackContext obj)
        {
            Debug.Log("Moving" + obj.ReadValue<Vector2>());
            currentDirection = obj.ReadValue<Vector2>();
            playerModel.PlayerMovementForce(obj.ReadValue<Vector2>());
        }

        public void PlayerInteractInput(InputAction.CallbackContext obj)
        {
            Debug.Log("Interacting");
        }
    }
}
