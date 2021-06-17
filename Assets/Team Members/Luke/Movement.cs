using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Luke
{
    public class Movement : MonoBehaviour
    {
        //References
        public NavMeshAgent navMeshAgent;
        
        //Variables
        public List<Waypoint> wayPoints;
        public int currentTarget;
        
        //Subscribe
        private void OnEnable()
        {
            throw new NotImplementedException();
        }

        //Unsubscribe
        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            GoToNextPoint();
        }

        // Update is called once per frame
        void Update()
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < .5f)
            {
                GoToNextPoint();
            }
        }

        // Movement stuff
        public void GoToNextPoint()
        {
            if (wayPoints.Count == 0)
            {
                return;
            }

            navMeshAgent.destination = wayPoints[currentTarget].transform.position;

            currentTarget = (currentTarget + 1) % wayPoints.Count;
        }
    }
}
