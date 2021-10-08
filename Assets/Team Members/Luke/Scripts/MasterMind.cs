using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Luke
{
    public class MasterMind : MonoBehaviour
    {
        //Variables
        private bool accusationCorrect = false;
        public List<NPCInformation> currentlyAccused;
        public List<List<bool>> accusationHistory;
        public List<SuspectIndividualButton> npcDetailsInstances;
        public List<bool> currentRoundBools;

        //events
        public event Action AllAccusedCorrectEvent;
        public event Action<NPCInformation> AddAccusedEvent;
        public event Action<NPCInformation> RemoveAccusedEvent;

        private void OnEnable()
        {
            foreach (SuspectIndividualButton suspectIndividualButton in npcDetailsInstances)
            {
                suspectIndividualButton.OnButtonPressAccuseEvent += AddToAccusationList;
            }
        }

        private void OnDisable()
        {
            foreach (SuspectIndividualButton suspectIndividualButton in npcDetailsInstances)
            {
                suspectIndividualButton.OnButtonPressAccuseEvent -= AddToAccusationList;
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

            if (!currentRoundBools.Contains(false))
            {
                AllAccusedCorrectEvent?.Invoke();
            }

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
