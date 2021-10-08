using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalModel : MonoBehaviour
{
    public PlayerJournal playerJournal;
    public SuspectIndividualButton suspectRef;
    public List<NPCInformation> npcInfos;
    
    //Suspect Page Variables
    public List<GameObject> suspectEntries;
    public List<Transform> suspectPagePositions;
    public GameObject currentSuspectReference;
    public int posCounter;
    public bool suspectTesting;
    
    //Individual Suspect Details 
    public TextMeshProUGUI suspectName;
    public TextMeshProUGUI suspectLocations;
    public RawImage mugShot;
    
    //Accusations
    public MasterMind masterMind;

    private void Awake()
    {
        npcInfos = playerJournal.npcInformation;
    }
    

    //Suspect Page 
    //Small Portraits for the suspect page with all the suspects
    public void SuspectPageIndividuals()
    {
        if (npcInfos != null)
        {
            
            foreach (NPCInformation npcInfo in npcInfos)
            {
                //TODO update with list of transforms for spawning
                if (suspectPagePositions.Count <= posCounter)
                {
                    GameObject tempSuspectEntry = Instantiate(currentSuspectReference, suspectPagePositions[posCounter]);
                    suspectEntries.Add(tempSuspectEntry);
                    tempSuspectEntry.GetComponent<SuspectIndividualButton>().npcInformation = npcInfo;
                    tempSuspectEntry.GetComponentInChildren<RawImage>().texture = npcInfo.mugShot;
                    tempSuspectEntry.GetComponentInChildren<TextMeshProUGUI>().text = npcInfo.suspectName;
                    tempSuspectEntry.GetComponent<SuspectIndividualButton>().OnButtonPressDetailsEvent +=
                        UpdateSuspectsDetails;
                    posCounter++;
                }
            }
        }
    }
    
    //Individual Suspect Details
    //Expanded details for the player to read
    public void UpdateSuspectsDetails(NPCInformation npcInfo)
    {
        suspectName.text = npcInfo.suspectName;
        for (int i = 0; i < npcInfos[i].locations.Count; i++)
        {
            suspectLocations.text = npcInfo.locations[i];
        }
        mugShot.texture = npcInfo.mugShot;
    }
}
