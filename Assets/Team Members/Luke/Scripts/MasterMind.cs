using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Luke
{
    public class MasterMind : MonoBehaviour
    {
        //References
        public JournalModel journalModel;
        public GameManager gameManager;
        public GameObject histroyTextRef;

        //Variables
        private bool accusationCorrect = false;
        public List<NPCInformation> currentlyAccused;
        public List<List<bool>> accusationHistory;
        [Tooltip("Positions of where the round accusation results display (how many correct and wrong each round)")]
        public List<GameObject> historyPositions;
        public List<GameObject> npcDetails;
        public List<bool> currentRoundBools;
        public int heistCounter = 0;
        

        //events
        public event Action AllAccusedCorrectEvent;
        public event Action FinaliseAccusationsEvent;
        public event Action<NPCInformation> AddAccusedEvent;
        public event Action RemoveAccusedEvent;

        private void Start()
        {
            accusationHistory = new List<List<bool>>();
        }

        private void OnEnable()
        {
            gameManager.GameSwitchSceneEvent += DisplayHistory;
            npcDetails = journalModel.suspectEntries;
            //StartCoroutine(Subscribing());
        }

        private void OnDisable()
        {
            gameManager.GameSwitchSceneEvent += DisplayHistory;
        }

        /*public IEnumerator Subscribing()
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject suspect in npcDetails)
            {
                suspect.GetComponent<SuspectIndividualButton>().OnButtonPressAccuseEvent += AddToAccusationList;
            }
        }*/

        public void AddToAccusationList(NPCInformation accusedDetails)
        {
            if (!currentlyAccused.Contains(accusedDetails) && currentlyAccused.Count < 4)
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

            foreach (bool roundBool in currentRoundBools)
            {
                if (roundBool)
                {
                    heistCounter++;
                }
            }

            if (heistCounter == 4)
            {
                AllAccusedCorrectEvent?.Invoke();
            }

            //if the player hasn't accused anyone
            if (currentlyAccused.Count >= 0)
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
        
        /// <summary>
        /// Displays the amount of correct and incorrect guesses each round
        /// </summary>
        public void DisplayHistory()
        {
            int falseCounter = 0;
            int trueCounter = 0;

            if (accusationHistory.Count != 0 && accusationHistory != null)
            {
                foreach (bool accusation in accusationHistory[gameManager.roundCounter -2])
                {
                    if (accusation)
                    {
                        trueCounter++;
                    }
                    else
                    {
                        falseCounter++;
                    }
                }

                GameObject tempHistoryText = Instantiate(histroyTextRef,historyPositions[gameManager.roundCounter -2].transform);
                tempHistoryText.GetComponent<TextMeshProUGUI>().text = trueCounter + " correct" + falseCounter + " incorrect";
            }
        }
    }
}
