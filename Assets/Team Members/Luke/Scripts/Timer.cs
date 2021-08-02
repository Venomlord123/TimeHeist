using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Luke
{
    public class Timer : MonoBehaviour
    {
        //Reference
        public GameManager gameManager;
        [Tooltip("TextMeshUGUI Reference")]
        public TextMeshProUGUI timerText;

        //variables
        [Tooltip("In seconds")]
        public float currentTime;
        [Tooltip("In seconds")]
        public float maxTime;
        public bool timeStarted;
        public float minutes;
        public float seconds;
        public float milliSeconds;
        
        //events
        public event Action TimerEndEvent;

        void Start()
        {
            currentTime = maxTime;
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timeStarted)
            {
                UpdateTime(currentTime);
            }
        }
        
        private void OnEnable()
        {
            gameManager.GameStartEvent += StartTime;
            gameManager.GamePauseEvent += PauseTime;
            gameManager.ResetLevelEvent += ResetTime;
        }

        private void OnDisable()
        {
            gameManager.GameStartEvent -= StartTime;
            gameManager.GamePauseEvent -= PauseTime;
            gameManager.ResetLevelEvent -= ResetTime;
        }
        
        //use for in between rounds in the main scene
        public void ResetTime()
        {
            currentTime = maxTime;
        }

        public void StartTime()
        {
            timeStarted = true;
        }

        public void PauseTime()
        {
            timeStarted = false;
        }

        public void UpdateTime(float displayTime)
        {
            if (timeStarted)
            {
                currentTime -= Time.deltaTime;
            
                //making the timer have minutes and seconds limits
                minutes = Mathf.FloorToInt(displayTime / 60);
                seconds = Mathf.FloorToInt(displayTime % 60);
            
                milliSeconds = (displayTime % 1) * 1000;
            
                if (currentTime <= 0f)
                {
                    currentTime = 0f;
                    //forcing the milliseconds because sometimes it gets stuck on a > 0 time.
                    milliSeconds = 0f;
                    //Game over stuff wants to know this
                    PauseTime();
                    TimerEndEvent?.Invoke();
                }
            }
        }
    }
}
