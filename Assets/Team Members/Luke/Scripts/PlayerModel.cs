using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class PlayerModel : MonoBehaviour
    {
        //references
        
        
        //Variables
        public float movementSpeed;
        public float currentSpeed;
        public Rigidbody rb;
        public CharacterController characterController;
        
        //Events
        
        
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayerMovementForce(Vector2 direction)
        {
            characterController.Move(direction * movementSpeed * Time.deltaTime);
            //rb.AddRelativeForce(direction * movementSpeed * Time.deltaTime);
        }
    }
}
