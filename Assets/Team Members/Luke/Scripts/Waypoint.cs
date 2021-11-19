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
        [Tooltip("When true the NPC arriving here will use the talking animation (animation 1)")]
        public bool conversation;
        [Header("Non-Waiters Animation Only")]
        [Tooltip("When true the NPC arriving here will use the talking animation (animation 2)")]
        public bool conversation2;
        [Tooltip("NPC arriving here will activate animation for observing art")]
        public bool observing;
    }
}

