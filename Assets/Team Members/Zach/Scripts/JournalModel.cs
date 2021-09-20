using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalModel : MonoBehaviour
{
    public PlayerJournal playerJournal;
    public SuspectReference suspectRef;
    public List<NPCInformation> npcInfos;
    
    //Suspect Page Variables
    public List<GameObject> suspectEntries;
    public GameObject currentSuspectReference;
    public bool suspectTesting;
    
    //Individual Suspect Details 
    public TextMeshProUGUI suspectName;
    public TextMeshProUGUI suspectLocations;
    public RawImage mugShot;

    private void Awake()
    {
        playerJournal.npcInformation = npcInfos;
    }

    private void OnEnable()
    {
         suspectRef.OnButtonPressDetailsEvent += UpdateSuspectsDetails;
    }

    private void OnDisable()
    {
        suspectRef.OnButtonPressDetailsEvent -= UpdateSuspectsDetails;
    }

    //Suspect Page 
    public void SuspectPageIndividuals()
    {
        if (npcInfos != null)
        {
            foreach (NPCInformation npcInfo in npcInfos)
            {
                //TODO update with list of transforms for spawning
                GameObject tempSuspectEntry = Instantiate(currentSuspectReference, gameObject.transform);
                suspectEntries.Add(tempSuspectEntry);
                tempSuspectEntry.GetComponent<SuspectReference>().npcInformation = npcInfo;
                tempSuspectEntry.GetComponentInChildren<RawImage>().texture = npcInfo.mugShot;
                tempSuspectEntry.GetComponentInChildren<TextMeshProUGUI>().text = npcInfo.suspectName;
                //Todo Test this and see if needs to be removed (likely)
                suspectTesting = false;
            }
        }
    }
    
    //Individual Suspect Details
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
