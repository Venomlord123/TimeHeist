using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class Waypoint : MonoBehaviour
    {
        [Tooltip("The exit waypoint triggers on the fire alarm")]
        public bool isExit = false;
        [Header("NPC Animation")]
        [Tooltip("If this is true the the NPC arriving here will use the talking animation")]
        public bool conversation;
        [Tooltip("NPC arriving here will activate animation for observing art")]
        public bool observing;
        [Tooltip("How long until the start of the talking or observing animation is started after arriving to the waypoint")]
        public float animationDelay;
    }
}

