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
        [Tooltip("Drag the player object here")]
        public PlayerMovementTimeStop playerMovementTimeStop;
        [Tooltip("Drag this character's animator here")]
        public Animator animator;

        //Variables
        [Tooltip("The current selected waypoint element")]
        public int currentTarget;
        [Tooltip(("Distance from waypoint to allow change to next waypoint"))]
        public float remainingWaypointDistance = .5f;
        [Tooltip("Change the npc's speed (multiplied from player speed)")]
        public float npcMovementMultiplier;
        [Tooltip("The walking animation speed (Higher the number slower it animates)")]
        public float animationSpeedDivider = 5f;
        private float waitTimeElement;
        [Tooltip("The current amount of time waiting at waypoint")]
        public float currentWaitTime;
        [Tooltip("Is the NPC waiting?")]
        public bool waiting = false;
        [Tooltip("Starting Position for the NPC")]
        public Vector3 startPosition;
        [Tooltip("Start rotation position of npc")]
        public Quaternion startRotation;
        
        

        //Subscribe
        private void OnEnable()
        {
            animator = GetComponentInChildren<Animator>();
            animator.speed = 0;
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
            startPosition = transform.position;
            startRotation = transform.rotation;
            
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
                    }
                }
            }
        }
        
        public void MovementStop(float speed)
        {
            animator.speed = 0;
            navMeshAgent.speed = 0;
            navMeshAgent.velocity = new Vector3(speed, speed, speed);
        }
        
        public void MovementContinue(float speed, Vector3 velocityNorm)
        {
            if (speed > 0)
            {
                navMeshAgent.speed = speed * npcMovementMultiplier;
                animator.speed = (animationSpeedDivider / velocityNorm.normalized.magnitude);
                if (waiting == false)
                {
                    //Animation walk
                    animator.SetBool("isWaiting", false);
                }
            }

            if (waiting)
            {
                //Animation idle
                animator.SetBool("isWaiting", true);
                
                RotateToDirection();
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
            //changed to here because of face direction
            currentTarget = (currentTarget + 1) % waypointPath.Count;
        }

        /// <summary>
        /// To face in the current waypoint's set directional vector forward (z)
        /// </summary>
        public void RotateToDirection()
        {
            Vector3 lookDirection = waypointPath[currentTarget].transform.forward; 
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, animator.speed / animationSpeedDivider);
            
        }
    }
}
