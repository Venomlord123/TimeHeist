using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luke
{
    public class NPCManager : MonoBehaviour
    {
        //variables 
        public List<NPCBase> allNpcs;
        public List<NPCBase> simpleNpcs;
        public List<NPCBase> redherringNpcs;
        public List<NPCBase> heistNpcs;

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
