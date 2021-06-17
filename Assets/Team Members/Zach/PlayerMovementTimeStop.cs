using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZachFrench
{
    
    
    public class PlayerMovementTimeStop : MonoBehaviour
    {

        //Event created using a bool
        public event Action timeStopEvent;
        public event Action continueTimeEvent;

        //Variables 
        public bool notMoving;
        public Vector3 lastPosition;

        // Start is called before the first frame update
        void Start()
        {
            notMoving = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (transform.position != lastPosition)
            {
                //player is moving
                notMoving = false;
                ContinueTime();
            }
            else //player isn't moving
            {
                notMoving = true;
                TimeStopping();
            }
            lastPosition = transform.position;
        }

        public void TimeStopping()
        {
            if (notMoving)
            {
                timeStopEvent.Invoke();
            }
        }

        public void ContinueTime()
        {
            if (notMoving == false)
            {
                continueTimeEvent.Invoke();
            }
        }
    }
}
