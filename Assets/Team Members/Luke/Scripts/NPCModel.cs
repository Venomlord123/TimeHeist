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
        [Tooltip("The npc's set exit point (On fire alarm event)")]
        public int setExitWaypoint;
        [Tooltip(("Distance from waypoint to allow change to next waypoint"))]
        public float remainingWaypointDistance = .5f;
        [Tooltip("Change the npc's speed (multiplied from player speed)")]
        public float npcMovementMultiplier;
        [Tooltip("The walking animation speed (Higher the number slower it animates)")]
        public float animationSpeedDivider = 5f;
        [Tooltip("The current amount of time waiting at waypoint (For visual testing purposes)")]
        public float currentWaitTime;
        [Tooltip("Is the NPC waiting?")]
        public bool waiting = false;
        [Tooltip("Starting Position for the NPC")]
        public Vector3 startPosition;
        [Tooltip("Start rotation position of npc")]
        public Quaternion startRotation;
        [Tooltip("If fire alarm is currently on")]
        public bool fireAlarmActive = false;
        [Tooltip("This npc will currently ignore the fire alarm exit waypoints")]
        public bool ignoreFireAlarm = false;

        public float counter;

        private bool isTalking = false;
        private bool isObserving = false;
        private int waitTimeElement;
        private float time = 0.178f;

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
            if (fireAlarmActive)
            {
                if (ignoreFireAlarm)
                {
                    GoToNextPoint();
                }
                else
                {
                    StartCoroutine(GoToExitPoint());
                }
            }
            else
            {
                GoToNextPoint();
            }
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
                        if (waypointPath[currentTarget].conversation)
                        {
                            Talking();
                        }

                        if (waypointPath[currentTarget].observing)
                        {
                            Observing();
                        }
                        
                        WaypointWaitTimer();
                        
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
        /// HACK needs a cleanup and a counter instead of IEnumarator
        public void WaypointWaitTimer()
        {
            waiting = true;
            navMeshAgent.isStopped = true;
            if (currentWaitTime == 0f)
            {
                currentWaitTime = time;
            }

            //TODO
            if (waiting)
            {
                counter = waypointWaitTimes[waitTimeElement];
            }
            
            if (Timer.timerOn)
            {
                if (waypointWaitTimes != null)
                {
                    counter = counter - Time.deltaTime;
                }
                
                navMeshAgent.isStopped = false;

                //changing npc wait to the next in list ready for next waiting period
                if (waypointWaitTimes.Count != 0 && waypointWaitTimes.Count < waypointWaitTimes.Capacity)
                {
                    waitTimeElement++;
                    currentWaitTime = waypointWaitTimes[waitTimeElement];
                }
                waiting = false;
                //changed to here because of face direction
                currentTarget = (currentTarget + 1) % waypointPath.Count;
            }
            
            //yield return new WaitForSeconds(currentWaitTime);
            
            if (fireAlarmActive && ignoreFireAlarm == false)
            {
                navMeshAgent.isStopped = false;
                waiting = false;
                currentTarget = setExitWaypoint;
            }
        }

        /// <summary>
        /// To face in the current waypoint's set directional vector forward (z)
        /// </summary>
        public void RotateToDirection()
        {
            Vector3 lookDirection = new Vector3();
            if (fireAlarmActive && ignoreFireAlarm == false)
            {
                lookDirection = exitWaypoints[setExitWaypoint].transform.forward;
            }
            else
            {
                lookDirection = waypointPath[currentTarget].transform.forward;
            }
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, animator.speed / animationSpeedDivider);
        }

        public IEnumerator GoToExitPoint()
        {
            if (ignoreFireAlarm == false)
            {
                yield return new WaitForSeconds(.5f);
                navMeshAgent.SetDestination(exitWaypoints[currentTarget].transform.position);
            }
        }

        public void Talking()
        {
            waiting = true;
            float counter = Timer.currentTimer - waypointPath[currentTarget].animationTimer;

            if (isTalking == false)
            {
                isTalking = true;
                animator.SetBool("isTalking", true);
            }

            if (Timer.currentTimer < counter)
            {
                isTalking = false;
                animator.SetBool("isTalking", false);
                waiting = false;
            }
        }

        public void Observing()
        {
            waiting = true;
            float counter = Timer.currentTimer - waypointPath[currentTarget].animationTimer;

            if (isObserving == false)
            {
                isObserving = true;
                animator.SetBool("isObserving", true);
            }

            if (Timer.currentTimer < counter)
            {
                isObserving = false;
                animator.SetBool("isObserving", false);
                waiting = false;
            }
        }
    }
}
