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
        public int currentTarget;
        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            GoToNextPoint();
        }

        private void GoToNextPoint()
        {
            if (wayPoints.Count == 0)
            {
                return;
            }

            navMeshAgent.destination = wayPoints[currentTarget].transform.position;

            currentTarget = (currentTarget + 1) % wayPoints.Count;
        }

        // Update is called once per frame
        void Update()
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < .5f)
            {
                GoToNextPoint();
            }
        }

        // public void NPCMovement()
        // {
        //     Vector3 dist = Vector3.Distance(wayPoints[currentTarget].transform.position, transform.position);
        //     currentTarget = wayPoints[0];
        //     
        //     if (navMeshAgent.remainingDistance < .2f)
        //     {
        //         for (int i = 0; i < wayPoints.Count; i++)
        //         {
        //             print(i);
        //             currentTarget = wayPoints[i].transform.position;
        //             
        //             if (navMeshAgent.remainingDistance < .2f)
        //             {
        //                 navMeshAgent.SetDestination(currentTarget);
        //             }
        //         }
        //     }
        // }
    }
}
