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

        private void OnEnable()
        {
            timer.TimerEndEvent += zeroTimer;
        }

        private void OnDisable()
        {
            timer.TimerEndEvent -= zeroTimer;
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
            //TEST ONLY 
            timer.timerText.text = string.Format("{0:0}:{1:00}:{2:000}", timer.timerMinutes, timer.timerSeconds, timer.timerMilliSeconds);
        }

        //force the zero
        public void zeroTimer()
        {
            timer.countDownText.text = string.Format("{0:0}:{1:00}:{2:000}", 0,0,0);
        }
    }
}
