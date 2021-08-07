using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZachFrench
{
    public class PlayerMovementTimeStop : MonoBehaviour
    {

        //Event created using a bool
        public event Action<float> TimeStopEvent;
        public event Action<float> ContinueTimeEvent;

        public event Action<Vector3> PassingNormalEvent;

        //References
        public PlayerModel playerModel;
        public CharacterController characterController;
        
        //Variables 
        [Tooltip("A bool to show if we are not moving")]
        public bool notMoving;
        private Vector3 lastPosition;

        // Start is called before the first frame update
        void Start()
        {
            notMoving = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (characterController.velocity.magnitude > .2f)
            {
                notMoving = false;
                ContinueTime();
            }
            else
            {
                notMoving = true;
                TimeStopping();
            }
        }

        public void TimeStopping()
        {
            if (notMoving)
            {
                TimeStopEvent?.Invoke(playerModel.velocity);
                //trigger fmod time stop
            }
        }

        public void ContinueTime()
        {
            if (notMoving == false)
            {
                ContinueTimeEvent?.Invoke(playerModel.velocity);
                PassingNormalEvent?.Invoke(playerModel.velocityNorm);
            }
        }
    }
}
