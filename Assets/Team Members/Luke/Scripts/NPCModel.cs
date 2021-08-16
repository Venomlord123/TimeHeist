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
        [Tooltip("The current selected waypoint element")]
        public int currentTarget;
        [Tooltip(("Distance from waypoint to allow change to next waypoint"))]
        public float remainingWaypointDistance = .5f;
        [Tooltip("Change the npc's speed (multiplied from player speed)")]
        public float npcMovementMultiplier = 1f;
        private float waitTimeElement;
        [Tooltip("The current amount of time waiting at waypoint")]
        public float currentWaitTime;
        [Tooltip("Is the NPC waiting?")]
        public bool waiting = false;

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
            Physics.IgnoreLayerCollision(7,7);
            //This is for when we need to reset to original positions
            startPos = transform.position;
            
            navMeshAgent = GetComponent<NavMeshAgent>();
            //setting our new variable to the NPCBase's first waypoint wait time
            if (waypointWaitTimes.Count != 0)
            {
                currentWaitTime = waypointWaitTimes[0];
            }
            GoToNextPoint();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GoToNextPoint();
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (waypointPath.Count != 0)
            {
                navMeshAgent.destination = waypointPath[currentTarget].transform.position;
                if (navMeshAgent.hasPath && navMeshAgent.remainingDistance < remainingWaypointDistance)
                {
                    if (waiting == false)
                    {
                        StartCoroutine(WaypointWaitTimer());
                        currentTarget = (currentTarget + 1) % waypointPath.Count;   
                    }
                }
            }
        }
        
        public void MovementStop(float speed)
        {
            navMeshAgent.speed = 0;
            navMeshAgent.velocity = new Vector3(speed, speed, speed);
        }
        
        public void MovementContinue(float speed)
        {
            if (speed > 0.01f)
            {
                navMeshAgent.speed = speed * npcMovementMultiplier;
            }
        }


        /// <summary>
        /// Co-routines only yield whats within the function!! 
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaypointWaitTimer()
        {
            navMeshAgent.isStopped = true;
            waiting = true;
            yield return new WaitForSeconds(currentWaitTime);
            navMeshAgent.isStopped = false;

            //changing npc wait to the next in list ready for next waiting period
            if (waypointWaitTimes.Count != 0 && waypointWaitTimes.Count < waypointWaitTimes.Capacity)
            {
                waitTimeElement++;
                currentWaitTime = waypointWaitTimes[(int)waitTimeElement];
            }
            waiting = false;
        }
    }
}
