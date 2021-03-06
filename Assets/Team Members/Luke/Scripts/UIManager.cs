using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZachFrench;

namespace Luke
{
    public class UIManager : MonoBehaviour
    {
        //references
        public GameManager gameManager;
        public PlayerModel playerModel;
        public Timer timer;
        public AudioManager audioManager;
        //Variables
        public GameObject eventTimer;
        public GameObject countDown;
        public GameObject SuspectPopUp;
        public TextMeshProUGUI gameEndText;
        public float timePopUpShown;

        private void OnEnable()
        {
            timer.CountDownEndEvent += DisablingCountDown;
            gameManager.JournalSwitchSceneEvent += DisablingCountDown;
            gameManager.JournalSwitchSceneEvent += ShowCountdown;
            playerModel.InteractEvent += InteractPopUp;
            gameManager.masterMind.AllAccusedCorrectEvent += GameEndPopUp;
        }

        private void Awake()
        {
            timer = FindObjectOfType<Timer>();
            gameManager = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void OnDisable()
        {
            timer.CountDownEndEvent -= DisablingCountDown;
            gameManager.JournalSwitchSceneEvent -= DisablingCountDown;
            gameManager.JournalSwitchSceneEvent -= ShowCountdown;
            playerModel.InteractEvent -= InteractPopUp;
        }

        // Update is called once per frame
        void Update()
        {
            PrintTimers();
        }

        public void PrintTimers()
        {
            // on the left 0 for the minutes and right of the colon is 1 for seconds
            //Player's visual
            timer.countDownText.text = string.Format("{0:0}:{1:00}", timer.minutesCountDown, timer.secondsCountDown);

            //level event timer 
            eventTimer.SetActive(true);
            timer.timerText.text = string.Format("{0:0}:{1:00}:{2:000}", timer.timerMinutes, timer.timerSeconds, timer.timerMilliSeconds);
        }
        
        public void DisablingCountDown()
        {
            //Forcing the zero view
            timer.countDownText.text = string.Format("{0:0}:{1:00}", 0f,0f);
            timer.timerText.text = string.Format("{0:0}:{1:00}:{2:000}", 0f, 0f, 0f);
            //disabling view of timer
            countDown.SetActive(false);
            eventTimer.SetActive(false);
            Debug.Log(eventTimer.GetComponent<TextMeshProUGUI>().text);
        }

        //enabling the countdown
        public void ShowCountdown()
        {
            eventTimer.SetActive(true);
            countDown.SetActive(true);
        }

        public void InteractPopUp()
        {
            //TODO check to see if the npc is already added
            if (audioManager.gameState == 1)
            {
                StartCoroutine(PopUpTimer());
                audioManager.SuspectAdded(); 
            }
        }

        public IEnumerator PopUpTimer()
        {
            SuspectPopUp.gameObject.SetActive(true);
            yield return new WaitForSeconds(timePopUpShown);
            SuspectPopUp.gameObject.SetActive(false);
        }

        public void GameEndPopUp()
        {
            gameEndText.gameObject.SetActive(true);
        }
    }
}
