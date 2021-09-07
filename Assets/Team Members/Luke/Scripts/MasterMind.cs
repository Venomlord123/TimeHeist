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
        public bool accusationCorrect = false;
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
        
        /// <summary>
        /// TODO UI
        /// </summary>
        public void CorrectAccusation()
        {
            accusationCorrect = true;
        }
        
        /// <summary>
        /// TODO UI
        /// </summary>
        public void WronglyAccused()
        {
            accusationCorrect = false;
        }

        /// <summary>
        /// TODO End game
        /// </summary>
        public void AllAccusedCorrect()
        {
            allHeistMembersAccused = true;
            AllAccusedCorrectEvent?.Invoke();
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
