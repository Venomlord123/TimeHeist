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
        public List<SuspectIndividualButton> npcDetailsInstances;
        public List<GameObject> npcDetails;
        public List<bool> currentRoundBools;
        

        //events
        public event Action AllAccusedCorrectEvent;
        public event Action FinaliseAccusationsEvent;
        public event Action<NPCInformation> AddAccusedEvent;
        public event Action<NPCInformation> RemoveAccusedEvent;

        private void Start()
        {
            journalModel = FindObjectOfType<JournalModel>();
        }

        private void OnEnable()
        {
            npcDetails = journalModel.suspectEntries;
            foreach (GameObject suspect in npcDetails)
            {
                suspect.GetComponent<SuspectIndividualButton>().OnButtonPressAccuseEvent += AddToAccusationList;
            }
        }

        private void OnDisable()
        {
            foreach (GameObject suspect in npcDetails)
            {
                suspect.GetComponent<SuspectIndividualButton>().OnButtonPressAccuseEvent -= AddToAccusationList;
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
        
        public void RemoveFromAccusationList(NPCInformation accusedDetails)
        {
            currentlyAccused.Clear();
            RemoveAccusedEvent.Invoke(accusedDetails);
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

            //Game ends
            if (!currentRoundBools.Contains(false))
            {
                AllAccusedCorrectEvent?.Invoke();
            }
            //Round ends
            else
            {
                FinaliseAccusationsEvent.Invoke();
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
