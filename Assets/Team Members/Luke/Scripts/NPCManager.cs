using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //variables 
        public List<NPCBase> allNpcs;
        public List<NPCBase> simpleNpcs;
        public List<NPCBase> redherringNpcs;
        public List<NPCBase> heistNpcs;

        // Start is called before the first frame update
        void Start()
        {
            allNpcs.AddRange(FindObjectsOfType<NPCBase>());
            HeistMemberSearch();
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
