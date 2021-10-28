using System.Collections.Generic;
using Luke;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject journalCanvas;
    public JournalModel journalModel;
    public MasterMind masterMind;

    //MasterMind UI
    public int accusationPosCount;
    public List<GameObject> accusedSuspectMugshot;

    private void OnEnable()
    {
        gameManager.GameSwitchSceneEvent += JournalActivate;
        gameManager.JournalSwitchSceneEvent += JournalDeactivate;
        gameManager.JournalSwitchSceneEvent += RemoveAccusedMugshot;
        masterMind.AddAccusedEvent += CreateAccusedMugshot;
        masterMind.RemoveAccusedEvent += RemoveAccusedMugshot;
    }

    private void OnDisable()
    {
        gameManager.GameSwitchSceneEvent -= JournalActivate;
        gameManager.JournalSwitchSceneEvent -= JournalDeactivate;
        gameManager.JournalSwitchSceneEvent -= RemoveAccusedMugshot;
        masterMind.AddAccusedEvent -= CreateAccusedMugshot;
        masterMind.RemoveAccusedEvent -= RemoveAccusedMugshot;
    }


    private void JournalActivate()
    {
        journalCanvas.SetActive(true);
        JournalUpdateSuspects();
    }

    private void JournalDeactivate()
    {
        journalCanvas.SetActive(false);
        accusationPosCount = 0;
    }

    public void JournalUpdateSuspects()
    {
        journalModel.SuspectPageIndividuals();
    }

    //Mastermind
    public void CreateAccusedMugshot(NPCInformation accusedDetails)
    {
        if (accusationPosCount < accusedSuspectMugshot.Count)
        {
            if (accusedSuspectMugshot[accusationPosCount] != null)
            {
                accusedSuspectMugshot[accusationPosCount].GetComponent<RawImage>().texture = accusedDetails.mugShot;
                accusationPosCount++;
            }
        }
    }

    public void RemoveAccusedMugshot()
    {
        if (accusationPosCount <= accusedSuspectMugshot.Count)
        {
            foreach (GameObject accusedSuspect in accusedSuspectMugshot)
            {
                accusedSuspect.GetComponent<RawImage>().texture = null;
            }

            accusationPosCount = 0;
        }
    }
}