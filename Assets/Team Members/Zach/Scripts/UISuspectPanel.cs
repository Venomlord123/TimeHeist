using System.Collections;
using System.Collections.Generic;
using Luke;
using TMPro;
using UnityEngine;

namespace ZachFrench
{

    public class UISuspectPanel : MonoBehaviour
    {

        public List<NPCBase> npcBases;
        public int currentNpc;
        public NPCBase currentNPCSelected;
        public bool nextSuspect;
        public TextMeshProUGUI suspectName;
        public TextMeshProUGUI suspectLocations;
        
        // Start is called before the first frame update
        void Start()
        {
            currentNpc = 0;
            currentNPCSelected = npcBases[currentNpc];
            updateSuspects();
        }

        // Update is called once per frame
        void Update()
        {
            if (nextSuspect)
            {
                currentNpc++;
                if (currentNpc < npcBases.Count)
                {
                    currentNPCSelected = npcBases[currentNpc];
                    updateSuspects();
                }
                nextSuspect = false;
            }
        }

        public void updateSuspects()
        {
            suspectName.text = currentNPCSelected.npcName;
            suspectLocations.text = currentNPCSelected.locationsBeen;
        }
    }
}
