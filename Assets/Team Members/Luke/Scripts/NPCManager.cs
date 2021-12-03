using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        public List<NPCBase> heistNpcs;
        public NPCModel npcModelTemp;

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
                if (npcModel.swapModelActiveOnStart)
                {
                    npcModel.objectToSwap.SetActive(true);
                }

                if (npcModel.swapModelInactiveOnStart)
                {
                    npcModel.objectToSwap.SetActive(false);
                }
                npcModelTemp = npcModel;
                
                npcModel.fireAlarmActive = false;
                Rigidbody tempRb = npcModel.GetComponent<Rigidbody>();
                StartCoroutine(WaitForFrame());
                /*if (tempRb.position != npcModel.startPosition)
                {
                    tempRb.position = npcModel.startPosition;
                    Debug.Log(npcModel.gameObject.name + npcModel.transform.position);
                }*/
                npcModel.currentTarget = 0;
            }
        }

        public IEnumerator WaitForFrame()
        {
            yield return new WaitForEndOfFrame();
            foreach (NPCModel npcModel in allNpcs)
            {
                Physics.IgnoreLayerCollision(10,9, true);
                npcModel.GetComponent<NavMeshAgent>().enabled = false;
                //npcModel.GetComponent<CapsuleCollider>().enabled = false;
                Rigidbody tempRb = npcModel.GetComponent<Rigidbody>();
                //tempRb.isKinematic = true;
                npcModel.transform.position = npcModel.startPosition;
                //tempRb.position = npcModelTemp.startPosition;
                npcModel.transform.rotation = npcModel.startRotation;
                //npcModelTemp.Resetting();
                Physics.IgnoreLayerCollision(10,9, false);
                npcModel.GetComponent<NavMeshAgent>().enabled = true;
                //npcModelTemp.GetComponent<CapsuleCollider>().enabled = true;
                //tempRb.isKinematic = false;
                
            }
           
        }

        /// <summary>
        /// Setting the exit point for each of the npc's on the event FireAlarm
        /// </summary>
        public void SetToExitWaypoint()
        {
            foreach (NPCModel npcModel in allNpcs)
            {
                npcModel.fireAlarmActive = true;
                
                if (npcModel.ignoreFireAlarm == false)
                {
                    npcModel.exitWaypoints = patrolManager.NPCExitWaypoints;
                    npcModel.currentTarget = npcModel.setExitWaypoint;
                    //hack need to make the speed of the player calculate to the npc animation (fire alarm increase npc speed)
                    // npcModel.animationSpeedDivider /= .5f;
                    // npcModel.npcMovementMultiplier *= 2;
                }
            }
        }
        
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
