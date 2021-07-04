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
        private NavMeshAgent navMeshAgent;
        public PlayerMovementTimeStop playerMovementTimeStop;

        //Variables
        private int currentTarget;
        public float waypointWaitTime;
        public float remainingWaypointDistance;

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
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < remainingWaypointDistance)
            {
                StartCoroutine(WaypointWaitTimer());
            }
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (waypointPath.Count == 0 || waypointPath == null)
            {
                return;
            }

            navMeshAgent.destination = waypointPath[currentTarget].transform.position;

            currentTarget = (currentTarget + 1) % waypointPath.Count;
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
        
        public IEnumerator WaypointWaitTimer()
        {
            yield return new WaitForSeconds(waypointWaitTime);
            GoToNextPoint();
        }
    }
}
