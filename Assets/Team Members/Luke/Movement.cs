using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Luke
{
    public class Movement : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public List<Waypoint> wayPoints;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            NPCMovement();
        }

        public void NPCMovement()
        {
            //fixing
            int waypointCount = 0;
            navMeshAgent.SetDestination(wayPoints[waypointCount].transform.position);
            if (navMeshAgent.remainingDistance < .2f)
            {
                waypointCount++;
                navMeshAgent.SetDestination(wayPoints[waypointCount].transform.position);
            }
            
        }
    }
}
