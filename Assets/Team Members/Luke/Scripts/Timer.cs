using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZachFrench;


namespace Luke
{
    public class Timer : MonoBehaviour
    {
        //Reference
        public GameManager gameManager;
        public PlayerMovementTimeStop playerMovement;

        //Variables for Level event
        [Header("Timer for level events")] 
        public TextMeshProUGUI timerText;
        public bool timerOn;
        [Tooltip("In seconds")]
        public float currentTimer;
        [Tooltip("In seconds")]
        public float maxTime;
        [Tooltip("Time the blackout will happen")]
        public float blackOutTime;
        [Tooltip("Time the fire alarm will happen")]
        public float fireAlarmTime;
        [HideInInspector]
        public float timerMinutes;
        [HideInInspector]
        public float timerSeconds;
        [HideInInspector]
        public float timerMilliSeconds;
        
        

        //variables for player countdown
        [Header("Player's visual countdown")]
        public TextMeshProUGUI countDownText;
        public bool countDownStarted;
        [Tooltip("In seconds")]
        public float currentCountDown;
        [Tooltip("In seconds")]
        public float maxCountDown;
        [HideInInspector]
        public float minutesCountDown;
        [HideInInspector]
        public float secondsCountDown;
        [HideInInspector]
        public float milliSecondsCountDown;
        
        //events
        public event Action TimerEndEvent;
        public event Action BlackOutEvent;
        public event Action FireAlarmEvent;

        void Start()
        {
            playerMovement = FindObjectOfType<PlayerMovementTimeStop>();
            currentCountDown = maxCountDown;
            currentTimer = maxTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (countDownStarted)
            {
                UpdateCountDown(currentCountDown);
            }

            if (timerOn)
            {
                EventTimer(currentTimer);
            }
        }
        
        private void OnEnable()
        {
            gameManager.GameStartEvent += StartCountDown;
            gameManager.GamePauseEvent += PauseCountDown;
            gameManager.ResetLevelEvent += ResetCountdown;

            playerMovement.PassingNormalEvent += AdjustTimer;
            playerMovement.TimeStopEvent += StopTimer;
        }

        private void OnDisable()
        {
            gameManager.GameStartEvent -= StartCountDown;
            gameManager.GamePauseEvent -= PauseCountDown;
            gameManager.ResetLevelEvent -= ResetCountdown;
            
            playerMovement.PassingNormalEvent -= AdjustTimer;
            playerMovement.TimeStopEvent -= StopTimer;
        }
        
        //use for in between rounds in the main scene
        public void ResetCountdown()
        {
            currentCountDown = maxCountDown;
        }

        public void StartCountDown()
        {
            countDownStarted = true;
            timerOn = true;
        }

        public void PauseCountDown()
        {
            countDownStarted = false;
        }

        /// <summary>
        /// Display UI time to the player. Isn't effected by movement time stop
        /// </summary>
        public void UpdateCountDown(float displayCountDown)
        {
            if (countDownStarted)
            {
                currentCountDown -= Time.deltaTime;

                //making the countdown and timer have minutes and seconds limits
                minutesCountDown = Mathf.FloorToInt(displayCountDown / 60);
                secondsCountDown = Mathf.FloorToInt(displayCountDown % 60);
                milliSecondsCountDown = (displayCountDown % 1) * 1000;

                //player visual
                if (currentCountDown <= 0f)
                {
                    currentCountDown = 0f;
                    //forcing the milliseconds because sometimes it gets stuck on a > 0 time.
                    milliSecondsCountDown = 0f;
                    //Game over stuff wants to know this
                    PauseCountDown();
                    TimerEndEvent?.Invoke();
                }
            }
        }

        /// <summary>
        /// Event timer being the hidden from UI to make sure events like black out are shot at the right time.
        /// </summary>
        public void EventTimer(float timer)
        {
            if (timerOn)
            {
                //currentTimer -= Time.deltaTime;

                timerMinutes = Mathf.FloorToInt(timer / 60);
                timerSeconds = Mathf.FloorToInt(timer % 60);
                timerMilliSeconds = (timer % 1) * 1000;
                
                if (currentTimer == blackOutTime)
                {
                    BlackOutEvent?.Invoke();
                    Debug.Log("BlackOut!!!");
                }

                if (currentTimer == fireAlarmTime)
                {
                    FireAlarmEvent?.Invoke();
                    Debug.Log("Fire Alarm!!!");
                }
                
                //TODO check for player movement and compare time
            }
        }

        public void AdjustTimer(Vector3 velocity)
        {
            if (timerOn)
            {
                currentTimer -= velocity.magnitude * Time.deltaTime;
            }
        }

        public void StopTimer(float velocity)
        {
            timerOn = false;
        }
    }
}
