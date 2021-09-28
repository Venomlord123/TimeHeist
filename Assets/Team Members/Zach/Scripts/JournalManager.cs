using System;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject journalCanvas;
    public JournalModel journalModel;

    private void OnEnable()
    {
        gameManager.GameSwitchSceneEvent += JournalActivate;
        gameManager.JournalSwitchSceneEvent += JournalDeactivate;
    }

    private void OnDisable()
    {
        gameManager.GameSwitchSceneEvent -= JournalActivate;
        gameManager.JournalSwitchSceneEvent -= JournalDeactivate;
    }


    private void JournalActivate()
    {
        journalCanvas.SetActive(true); 
        JournalUpdateSuspects();
    }

    private void JournalDeactivate()
    {
        journalCanvas.SetActive(false);
    }

    public void JournalUpdateSuspects()
    {
        journalModel.SuspectPageIndividuals();
    }

}