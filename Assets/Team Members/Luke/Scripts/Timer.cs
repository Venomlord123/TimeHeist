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
        
        //Events TODO GameManager wants to know about these
        public event Action StartTimerEvent;
        public event Action PauseTimerEvent;
        public event Action StopTimerEvent;
        public event Action ResetTimerEvent;

        void Start()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
            //will need to change for triggering an event to start time
            StartTime();
        }

        // Update is called once per frame
        void Update()
        {
            if (timeStarted)
            {
                UpdateTime(currentTime);
            }
            PrintTimer();
        }
        
        //use for in between rounds in the main scene
        public void ResetTime()
        {
            ResetTimerEvent?.Invoke();
            currentTime = maxTime;
        }

        public void StartTime()
        {
            StartTimerEvent?.Invoke();
            timeStarted = true;
            currentTime = maxTime;
        }

        public void PauseTime()
        {
            PauseTimerEvent?.Invoke();
            timeStarted = false;
        }

        public void UpdateTime(float displayTime)
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
                StopTimerEvent?.Invoke();
            }
        }
        
        //Visuals for UI
        public void PrintTimer()
        {
            // on the left 0 for the minutes and right of the colon is 1 for seconds
            timerText.text = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
        }
    }
}
