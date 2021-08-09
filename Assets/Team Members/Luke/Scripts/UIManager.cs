using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class UIManager : MonoBehaviour
    {
        //references
        public Timer timer;
        public bool eventTimerShown;
        public GameObject eventTimer;
        public GameObject playerCountdown;

        private void OnEnable()
        {
            timer.CountDownEndEvent += ZeroCountDown;
        }

        private void OnDisable()
        {
            timer.CountDownEndEvent -= ZeroCountDown;
        }

        // Start is called before the first frame update
        void Start()
        {
            timer = FindObjectOfType<Timer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer.countDownStarted)
            {
                PrintTimer();
            }
        }

        public void PrintTimer()
        {
            // on the left 0 for the minutes and right of the colon is 1 for seconds
            //Player's visual
            timer.countDownText.text = string.Format("{0:0}:{1:00}:{2:000}", timer.minutesCountDown, timer.secondsCountDown, timer.milliSecondsCountDown);
            
            //level event timer 
            //DEBUG PURPOSE ONLY 
            if (eventTimerShown)
            {
                eventTimer.SetActive(true);
                timer.timerText.text = string.Format("{0:0}:{1:00}:{2:000}", timer.timerMinutes, timer.timerSeconds, timer.timerMilliSeconds);
            }
            else
            {
                eventTimer.SetActive(false);
            }
        }

        //force the zero
        public void ZeroCountDown()
        {
            timer.countDownText.text = string.Format("{0:0}:{1:00}:{2:000}", 0,0,0);
        }
    }
}
