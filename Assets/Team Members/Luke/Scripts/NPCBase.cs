using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Luke;
using UnityEngine;

namespace Luke
{
    public class NPCBase : MonoBehaviour
    {
        [Header("NPC Profile Information")]
        //Details for NPC's
        [Tooltip("Name of the NPC")]
        public string npcName;
        [Tooltip("Where the NPC's have been")]
        [TextArea]
        public string locationsBeen;
        [Tooltip("What has been said in the conversations that the NPC has interacted with")]
        public string conversations;
        [Tooltip("The current location of the NPC based on the trigger they are in")]
        public string currentLocation;
        
        [Header("NPC Movement Related")]
        //variables
        [Tooltip("Path of the NPC")]
        public List<Waypoint> waypointPath;
        [Tooltip("Wait times for each waypoint")]
        public List<float> waypointWaitTimes;
        [Tooltip("Starting locations of the NPC (reset positions)")]
        public Vector3 startPos;

        public void OnTriggerEnter(Collider other)
        {
            currentLocation = other.name;
        }
    }
}
