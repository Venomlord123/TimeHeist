using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalModel : MonoBehaviour
{
    public PlayerJournal playerJournal;
    public List<NPCInfomation> npcInfos;
    
    //Suspect Page Variables
    public List<GameObject> suspectEntries;
    public GameObject suspectReference;
    public bool suspectTesting;
    
    //Individual Suspect Details 
    public bool nextSuspect;
    public NPCInfomation currentNPCSelected;
    public TextMeshProUGUI suspectName;
    public TextMeshProUGUI suspectLocations;
    public RawImage mugShot;

    private void Awake()
    {
        playerJournal.npcInformation = npcInfos;
    }
    
    
    
    
    //Suspect Page 
    public void SuspectPageIndividuals()
    {
        if (npcInfos != null)
        {
            foreach (NPCInfomation npcInfo in npcInfos)
            {
                //TODO update with list of transforms for spawning
                GameObject tempSuspectEntry = Instantiate(suspectReference, gameObject.transform);
                suspectEntries.Add(tempSuspectEntry);
                tempSuspectEntry.GetComponentInChildren<RawImage>().texture = npcInfo.mugShot;
                tempSuspectEntry.GetComponentInChildren<TextMeshProUGUI>().text = npcInfo.suspectName;
                suspectTesting = false;
            }
        }
    }
    
    //Individual Suspect Details
    public void UpdateSuspects()
    {
        suspectName.text = currentNPCSelected.suspectName;
        for (int i = 0; i < npcInfos[i].locations.Count; i++)
        {
            suspectLocations.text = currentNPCSelected.locations[i];
        }
        mugShot.texture = currentNPCSelected.mugShot;
    }
}
