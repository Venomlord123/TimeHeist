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

        public GameManager gameManager;

        //variables
        [Tooltip("In seconds")]
        public float currentTime;
        [Tooltip("In seconds")]
        public float maxTime;
        public bool timeStarted;
        public float minutes;
        public float seconds;
        public float milliSeconds;
        
        //Events TODO GameManager wants to know about this
        public event Action StopTimerEvent;

        // private void OnEnable()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // private void OnDisable()
        // {
        //     throw new NotImplementedException();
        // }

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            timerText = GetComponentInChildren<TextMeshProUGUI>();
            //will need to change for triggering an event to start time
            StartTimer();
        }

        // Update is called once per frame
        void Update()
        {
            if (timeStarted)
            {
                UpdateTimer(currentTime);
            }
        }
        
        //use for in between rounds in the main scene
        public void ResetTimer()
        {
            currentTime = maxTime;
        }

        public void StartTimer()
        {
            timeStarted = true;
            currentTime = maxTime;
        }

        public void StopTimer()
        {
            timeStarted = false;
        }

        public void UpdateTimer(float displayTime)
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
                StopTimer();
                StopTimerEvent?.Invoke();
            }
        }
    }
}
