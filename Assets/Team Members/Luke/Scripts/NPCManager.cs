using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //variables 
        public GameManager gameManager;
        public List<NPCModel> allNpcs;
        public List<NPCBase> simpleNpcs;
        public List<NPCBase> redherringNpcs;
        public List<NPCBase> heistNpcs;

        // Start is called before the first frame update
        void Start()
        {
            allNpcs.AddRange(FindObjectsOfType<NPCModel>());
            HeistMemberSearch();
        }

        private void OnEnable()
        {
            gameManager.JournalSwitchSceneEvent += ResetOnJournalEnd;
        }
        
        private void OnDisable()
        {
            gameManager.JournalSwitchSceneEvent -= ResetOnJournalEnd;
        }
        
        private void ResetOnJournalEnd()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                Transform npcModelTransform = npcModel.transform;
                npcModelTransform.position = npcModel.startPosition.position;
                npcModelTransform.rotation = npcModel.startPosition.rotation;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// TODO find heist members from NPCBase then add to heist list
        /// </summary>
        public void HeistMemberSearch()
        {
            foreach (NPCBase npc in allNpcs)
            {
                if (npc.isHeistMember)
                {
                    heistNpcs.Add(npc);
                }
            }
        }
    }
}
