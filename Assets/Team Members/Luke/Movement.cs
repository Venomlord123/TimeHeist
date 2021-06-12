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
        public Vector3 currentTarget;
        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent.SetDestination(wayPoints[0].transform.position);
            currentTarget = wayPoints[0].transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            NPCMovement();
        }

        public void NPCMovement()
        {
            if (navMeshAgent.remainingDistance < .2f)
            {
                for (int i = 0; i < wayPoints.Count; i++)
                {
                    currentTarget = wayPoints[i].transform.position;
                    
                    if (navMeshAgent.remainingDistance < .2f)
                    {
                        navMeshAgent.SetDestination(currentTarget);
                    }
                }
            }
        }
    }
}
