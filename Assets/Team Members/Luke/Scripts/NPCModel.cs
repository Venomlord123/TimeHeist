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
        public int currentTarget;
        [Tooltip("Amount of time at waypoint")]
        public float waypointWaitTime = 3f;
        [Tooltip(("Distance from waypoint to allow change to next waypoint"))]
        public float remainingWaypointDistance = 2f;
        [Tooltip("Change the npc's speed (multiplied from player speed)")]
        public float npcMovementMultiplier = 1f;

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
            
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (waypointPath.Count != 0)
            {
                navMeshAgent.destination = waypointPath[currentTarget].transform.position;
                currentTarget = (currentTarget + 1) % waypointPath.Count;
                
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < remainingWaypointDistance)
                {
                    StartCoroutine(WaypointWaitTimer());
                }
            }
        }
        
        public void MovementStop(float speed)
        {
            navMeshAgent.speed = speed * npcMovementMultiplier;
        }
        
        public void MovementContinue(float speed)
        {
            if (speed > 0.01f)
            {
                navMeshAgent.speed = speed * npcMovementMultiplier;
            }
        }
        
        public IEnumerator WaypointWaitTimer()
        {
            yield return new WaitForSeconds(waypointWaitTime);
            GoToNextPoint();
        }
    }
}
