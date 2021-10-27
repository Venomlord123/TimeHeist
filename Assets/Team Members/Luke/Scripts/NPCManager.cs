using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //References
        public PatrolManager patrolManager;
        public GameManager gameManager;
        public Timer timer;

        //variables 
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
            timer.FireAlarmEvent += SetToExitWaypoint;
        }
        
        private void OnDisable()
        {
            gameManager.JournalSwitchSceneEvent -= ResetOnJournalEnd;
            timer.FireAlarmEvent -= SetToExitWaypoint;
        }
        
        private void ResetOnJournalEnd()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                npcModel.fireAlarmActive = false;
                npcModel.transform.position = npcModel.startPosition;
                npcModel.transform.rotation = npcModel.startRotation;
                npcModel.currentTarget = 0;
            }
        }

        /// <summary>
        /// Setting the exit point for each of the npc's on the event FireAlarm
        /// </summary>
        public void SetToExitWaypoint()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                npcModel.exitWaypoints = patrolManager.NPCExitWaypoints;
                npcModel.currentTarget = npcModel.setExitWaypoint;
                npcModel.fireAlarmActive = true;
            }
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
