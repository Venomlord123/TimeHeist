using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class PatrolManager : MonoBehaviour
    {
        //variables
        public List<Waypoint> NPCWayPoints;
        public List<Waypoint> NPCExitWaypoints;
        public float rayLength = 5f;
        
        // Start is called before the first frame update
        void Start()
        {
            NPCWayPoints.AddRange(FindObjectsOfType<Waypoint>());
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Waypoint npcWayPoint in NPCWayPoints)
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward) * rayLength;
                Debug.DrawRay(npcWayPoint.transform.position,forward,Color.blue);
            }
        }
    }
}
