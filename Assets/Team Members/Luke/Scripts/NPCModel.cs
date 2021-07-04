using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ZachFrench;

namespace Luke
{
    public class NPCModel : NPCBase
    {
        //References
        public NavMeshAgent navMeshAgent;
        public PlayerMovementTimeStop playerMovementTimeStop;

        //Variables
        public List<Waypoint> wayPoints;
        public int currentTarget;
        
        //Subscribe
        private void OnEnable()
        {
            playerMovementTimeStop.TimeStopEvent += MovementStop;
            playerMovementTimeStop.ContinueTimeEvent += MovementContinue;
        }

        //Unsubscribe
        private void OnDisable()
        {
            playerMovementTimeStop.TimeStopEvent -= MovementStop;
            playerMovementTimeStop.ContinueTimeEvent -= MovementContinue;
        }

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            GoToNextPoint();
        }

        // Update is called once per frame
        void Update()
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < .5f)
            {
                GoToNextPoint();
            }
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (wayPoints.Count == 0)
            {
                return;
            }

            navMeshAgent.destination = wayPoints[currentTarget].transform.position;

            currentTarget = (currentTarget + 1) % wayPoints.Count;
        }
        
        public void MovementStop(float speed)
        {
            navMeshAgent.speed = speed;
        }
        
        public void MovementContinue(float speed)
        {
            if (navMeshAgent.speed > 0.01f)
            {
                navMeshAgent.speed = speed;
            }
        }
    }
}
