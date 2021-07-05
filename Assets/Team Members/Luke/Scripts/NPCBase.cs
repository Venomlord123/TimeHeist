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
    }
}
