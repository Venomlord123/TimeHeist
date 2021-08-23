using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ZachFrench
{
    public class UISuspectPanel : MonoBehaviour
    {
        public List<NPCInfomation> npcInfo;
        public int currentNpc;
        public NPCInfomation currentNPCSelected;
        public bool nextSuspect;
        public TextMeshProUGUI suspectName;
        public TextMeshProUGUI suspectLocations;

        //Updates to use Player Journal
        public PlayerJournal playerJournal;
        public bool tempControl;


        // Start is called before the first frame update
        private void Start()
        {
            //Updates to use Player Journal
            playerJournal = FindObjectOfType<PlayerJournal>();
            npcInfo = playerJournal.npcInformation;
            /*currentNpc = 0;
            currentNPCSelected = npcInfo[currentNpc];
            UpdateSuspects();*/
        }

        // Update is called once per frame
        private void Update()
        {
            if (tempControl)
            {
                currentNPCSelected = npcInfo[currentNpc];
                UpdateSuspects();
                if (nextSuspect)
                {
                    currentNpc++;
                    if (currentNpc < npcInfo.Count)
                    {
                        currentNPCSelected = npcInfo[currentNpc];
                        UpdateSuspects();
                    }
                    nextSuspect = false;
                }
            }
        }

        public void UpdateSuspects()
        {
            suspectName.text = currentNPCSelected.suspectName;
            for (int i = 0; i < npcInfo[i].locations.Count; i++)
            {
                suspectLocations.text = currentNPCSelected.locations[i];
            }
        }
    }
}