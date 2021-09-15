using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Luke
{
    public class MasterMind : MonoBehaviour
    {
        //References
        public NPCManager npcManager;
        
        //Variables
        //TODO make individual bools for accusation either correct or false
        private bool accusationCorrect = false;
        public bool allHeistMembersAccused = false;
        public List<bool> currentlyAccused;
        public List<List<bool>> accusationHistory;
        
        //events
        public event Action AllAccusedCorrectEvent;

        // Start is called before the first frame update
        void Start()
        {
            npcManager = FindObjectOfType<NPCManager>();
        }

        //TODO below to be called when accusations is fully completed

        public void CheckAccusations()
        {
            //foreach (var VARIABLE in COLLECTION)
            {
                //if(var.ishiestmember)
                {
                    accusationCorrect = true;
                }
                //else
                {
                    accusationCorrect = false;
                }
            }
            AccusationsCorrect();
        }

        public void AccusationsCorrect()
        {
            //TODO set this bool
            if (allHeistMembersAccused)
            {
                AllAccusedCorrectEvent?.Invoke();
            }
        }

        /// <summary>
        /// TODO store the last set of accusations when finished
        /// </summary>
        public void StoreHistory()
        {
            accusationHistory.Add(currentlyAccused);
        }
    }
}
