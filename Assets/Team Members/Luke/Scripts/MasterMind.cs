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
        public JournalModel journalModel;
        
        //Variables
        private bool accusationCorrect = false;
        public List<NPCInformation> currentlyAccused;
        public List<List<bool>> accusationHistory;
        public List<GameObject> npcDetails;
        public List<bool> currentRoundBools;
        

        //events
        public event Action AllAccusedCorrectEvent;
        public event Action FinaliseAccusationsEvent;
        public event Action<NPCInformation> AddAccusedEvent;
        public event Action RemoveAccusedEvent;

        private void OnEnable()
        {
            npcDetails = journalModel.suspectEntries;
            StartCoroutine(Subscribing());
        }

        public IEnumerator Subscribing()
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject suspect in npcDetails)
            {
                suspect.GetComponent<SuspectIndividualButton>().OnButtonPressAccuseEvent += AddToAccusationList;
            }
        }

        public void AddToAccusationList(NPCInformation accusedDetails)
        {
            //TODO not sure if this checks for different type of accused details
            if (!currentlyAccused.Contains(accusedDetails))
            {
                currentlyAccused.Add(accusedDetails);
            }
            
            AddAccusedEvent.Invoke(accusedDetails);
        }
        
        public void RemoveFromAccusationList()
        {
            currentlyAccused.Clear();
            RemoveAccusedEvent.Invoke();
        }
        
        public void CheckAccusations()
        {
            currentRoundBools.Clear();
            
            foreach (NPCInformation npcInformation in currentlyAccused)
            {
                if(npcInformation.isHeistMember)
                {
                    accusationCorrect = true;
                }
                else
                {
                    accusationCorrect = false;
                }
                currentRoundBools.Add(accusationCorrect);
            }

            //Game ends (we won)
            if (!currentRoundBools.Contains(false))
            {
                AllAccusedCorrectEvent?.Invoke();
            }
            //Round ends
            else
            {
                FinaliseAccusationsEvent?.Invoke();
            }

            //History of accusations (true or false amount)
            StoreHistory(currentRoundBools);
        }

        /// <summary>
        /// stores the last round of accusations when finished
        /// </summary>
        public void StoreHistory(List<bool> currentlyAccused)
        {
            accusationHistory.Add(currentlyAccused);
        }
    }
}
