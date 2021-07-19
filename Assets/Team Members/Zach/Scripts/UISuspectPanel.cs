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
        public NPCBase currentNPC;
        public TextMeshProUGUI suspectName;
        public TextMeshProUGUI suspectLocations;
        
        // Start is called before the first frame update
        void Start()
        {
           npcBases.Add(FindObjectOfType<NPCBase>());
           currentNPC = npcBases[0];
        }

        // Update is called once per frame
        void Update()
        {
            suspectName.text = currentNPC.npcName;
            suspectLocations.text = currentNPC.locationsBeen;
        }
    }
}
