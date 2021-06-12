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
    }
}
