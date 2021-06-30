using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //variables 
        public List<NPCBase> allNpcs;

        // Start is called before the first frame update
        void Start()
        {
            allNpcs.AddRange(FindObjectsOfType<NPCBase>());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
