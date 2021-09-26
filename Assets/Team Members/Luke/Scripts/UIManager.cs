using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZachFrench;

namespace Luke
{
    public class UIManager : MonoBehaviour
    {
        //references
        public GameManager gameManager;
        public Timer timer;
        public bool eventTimerShown;
        public GameObject eventTimer;
        public GameObject countDown;

        private void OnEnable()
        {
            timer.CountDownEndEvent += ZeroCountDown;
            gameManager.GameSwitchSceneEvent += ZeroCountDown;
            gameManager.JournalSwitchSceneEvent += ShowTimer;
        }

        private void OnDisable()
        {
            timer.CountDownEndEvent -= ZeroCountDown;
            gameManager.GameSwitchSceneEvent -= ZeroCountDown;
            gameManager.JournalSwitchSceneEvent -= ShowTimer;
        }

        // Start is called before the first frame update
        void Start()
        {
            timer = FindObjectOfType<Timer>();
            gameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //TODO 
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
        
        public void ZeroCountDown()
        {
            //Forcing the zero view
            timer.countDownText.text = string.Format("{0:0}:{1:00}:{2:000}", 0,0,0);
            //disabling view of timer
            countDown.SetActive(false);
        }

        //enabling the timer
        public void ShowTimer()
        {
            countDown.SetActive(true);
        }
    }
}
