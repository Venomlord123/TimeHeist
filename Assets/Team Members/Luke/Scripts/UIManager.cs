using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class UIManager : MonoBehaviour
    {
        //references
        public Timer timer;
        
        // Start is called before the first frame update
        void Start()
        {
            timer = FindObjectOfType<Timer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timer.timeStarted)
            {
                PrintTimer();
            }
        }

        public void PrintTimer()
        {
            // on the left 0 for the minutes and right of the colon is 1 for seconds
            timer.timerText.text = string.Format("{0:0}:{1:00}:{2:000}", timer.minutes, timer.seconds, timer.milliSeconds);
        }
    }
}
