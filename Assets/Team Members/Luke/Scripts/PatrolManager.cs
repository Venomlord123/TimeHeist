using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class PatrolManager : MonoBehaviour
    {
        //variables
        public List<Waypoint> NPCWayPoints;
        
        // Start is called before the first frame update
        void Start()
        {
            NPCWayPoints.AddRange(GetComponents<Waypoint>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
