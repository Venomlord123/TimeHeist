using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;

namespace Luke
{
    public class NPCBase : MonoBehaviour
    {
        //variables
        [Tooltip("Path of the NPC")]
        public List<Waypoint> waypointPath;
        [Tooltip("Wait times for each waypoint")]
        public List<float> waypointWaitTimes;
        
        //Details for NPC's
        [Tooltip("Name of the NPC")]
        public string npcName;
        [Tooltip("Where the NPC's have been")]
        public string locationsBeen;
        [Tooltip("What has been said in the conversations that the NPC has interacted with")]
        public string conversations;
    }
}
