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
        public List<Waypoint> waypointPath;

        public List<float> waypointWaitTimes;
        //public float npcSpeed;
        //public float WaypointWaitTime;

        private void Start()
        {
            foreach (Waypoint waypoint in waypointPath)
            {
                waypointWaitTimes.Capacity++;
            }
        }
    }
}
