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
        [Tooltip("Drag the audio source on the npc here")]
        public AudioSource voiceAudio;

        //Variables
        [Tooltip("The current selected waypoint element")]
        public int currentTarget;

        [Tooltip("The npc's set exit point (On fire alarm event)")]
        public int setExitWaypoint;

        [Tooltip("Distance from waypoint to allow change to next waypoint")]
        public float remainingWaypointDistance = .5f;

        [Tooltip("Change the npc's speed (multiplied from player speed)")]
        public float npcMovementMultiplier;

        [Tooltip("The walking animation speed (Higher the number slower it animates)")]
        public float animationSpeedDivider = 5f;

        [Tooltip("The current amount of time waiting at waypoint (For visual testing purposes)")]
        public float currentWaitTime;

        [Tooltip("Is the NPC waiting? (testing purposes)")]
        public bool waiting;

        [Tooltip("Starting Position for the NPC")]
        [SerializeField] public Vector3 startPosition;

        [Tooltip("Start rotation position of npc")]
        public Quaternion startRotation;

        [Tooltip("If fire alarm is currently on")]
        public bool fireAlarmActive;

        [Tooltip("This npc will currently ignore the fire alarm exit waypoints")]
        public bool ignoreFireAlarm;
        [Tooltip("An array that holds all the characters needed audio clips")]
        public List<AudioClip> audioSources;

        public float counter;
        private bool counterStarted;
        private bool isObserving;
        private bool isTalking;
        private float time = 0.178f;
        private int waitTimeElement;

        // Start is called before the first frame update
        private void Start()
        {
            Physics.IgnoreLayerCollision(7, 7);
            //This is for when we need to reset to original positions
            startPosition = transform.position;
            startRotation = transform.rotation;

            navMeshAgent = GetComponent<NavMeshAgent>();
            voiceAudio = GetComponent<AudioSource>();
            //setting our new variable to the NPCBase's first waypoint wait time
            if (waypointWaitTimes.Count != 0) currentWaitTime = waypointWaitTimes[0];
            GoToNextPoint();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (fireAlarmActive)
            {
                if (ignoreFireAlarm)
                    GoToNextPoint();
                else
                    //todo PROBABLY DOESN'T NEED TO BE AN IENUMURATOR
                    StartCoroutine(GoToExitPoint());
            }
            else
            {
                GoToNextPoint();
            }

            if (waypointWaitTimes != null && counterStarted && waiting)
            {
                if (Timer.timerOn)
                {
                    counter = counter - Time.deltaTime;
                    if (counter < 0f) WaypointWaitTimer();
                }
            }
                
        }

        //Subscribe
        private void OnEnable()
        {
            animator = GetComponentInChildren<Animator>();
            animator.speed = 0;
            PlayerMovementTimeStop.TimeStopEvent += MovementStop;
            PlayerMovementTimeStop.ContinueTimeEvent += MovementContinue;
        }

        //Unsubscribe
        private void OnDisable()
        {
            PlayerMovementTimeStop.TimeStopEvent -= MovementStop;
            PlayerMovementTimeStop.ContinueTimeEvent -= MovementContinue;
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (waypointPath.Count != 0)
            {
                navMeshAgent.destination = waypointPath[currentTarget].transform.position;
                if (navMeshAgent.hasPath && navMeshAgent.remainingDistance < remainingWaypointDistance)
                    if (waiting == false)
                    {
                        WaypointWaitTimer();
                        if (waypointPath[currentTarget].conversation) Talking();

                        if (waypointPath[currentTarget].observing) Observing();
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
                animator.speed = animationSpeedDivider / velocityNorm.normalized.magnitude;
                if (waiting == false)
                    //Animation walk
                    animator.SetBool("isWaiting", false);
            }

            if (waiting)
            {
                //Animation idle
                animator.SetBool("isWaiting", true);

                RotateToDirection();
            }
        }

        /// <summary>
        ///     Co-routines only yield whats within the function!!
        ///     HACK needs a cleanup and a counter instead of IEnumarator
        public void WaypointWaitTimer()
        {
            waiting = true;
            navMeshAgent.isStopped = true;
            /*if (currentWaitTime == 0f)
            {
                currentWaitTime = time;
            }*/

            //TODO
            if (waiting)
            {
                if (counterStarted == false && waiting)
                {
                    counter = waypointWaitTimes[waitTimeElement];
                    counterStarted = true;
                }

                if (counter < 0f)
                {
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
                    counterStarted = false;
                }
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
        ///     To face in the current waypoint's set directional vector forward (z)
        /// </summary>
        public void RotateToDirection()
        {
            var lookDirection = new Vector3();
            if (fireAlarmActive && ignoreFireAlarm == false)
                lookDirection = exitWaypoints[setExitWaypoint].transform.forward;
            else
                lookDirection = waypointPath[currentTarget].transform.forward;
            var newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, newRotation, animator.speed / animationSpeedDivider);
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
            if (isTalking == false)
            {
                isTalking = true;
                animator.SetBool("isTalking", true);
            }

            if (Timer.currentTimer < counter)
            {
                isTalking = false;
                animator.SetBool("isTalking", false);
            }
        }

        public void Observing()
        {
            if (isObserving == false)
            {
                isObserving = true;
                animator.SetBool("isObserving", true);
            }

            if (Timer.currentTimer < counter)
            {
                isObserving = false;
                animator.SetBool("isObserving", false);
            }
        }
    }
}