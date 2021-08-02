using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;

public class PlayerJournal : MonoBehaviour
{
    public List<NPCBase> npcBases;
    public List<NPCInfomation> npcInformation;
    public GameObject npcInstance;
    public GameObject tempNpcInfoGameObject;


    public void GatheredInformation(NPCBase npcBase)
    {
        if (npcBases.Contains(npcBase) != npcBase)
        {
            npcBases.Add(npcBase);
            tempNpcInfoGameObject = Instantiate(npcInstance, transform.position, new Quaternion(0, 0, 0, 0));
            tempNpcInfoGameObject.transform.parent = this.transform;
            npcInformation.Add(tempNpcInfoGameObject.GetComponent<NPCInfomation>());
        
            //todo finish writing all relevant information
            tempNpcInfoGameObject.GetComponent<NPCInfomation>().suspectName = npcBase.npcName;
        }else if (npcBases.Contains(npcBase) == npcBase)
        {
            //todo add the overwrites for the locations and conversations if applicable 
        }
    }
    
}
