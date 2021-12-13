using Luke;
using ZachFrench;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueEventHandler : MonoBehaviour
{
    [HideInInspector]
    public Timer timer;
    // public Pause pause;

    [FMODUnity.EventRef]
    public string FMODDialogueEvent;
    private FMOD.Studio.EventInstance FMODDialogueInstance;

    [Tooltip("Choose an integer in seconds. Range: 0-70")]
    public int dialogueEventTime;

    private bool dialogueEventDone = false;

    void Start() 
    {
        FMODDialogueInstance = FMODUnity.RuntimeManager.CreateInstance (FMODDialogueEvent);
    }

    void Update() 
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FMODDialogueInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (Timer.currentTimer <= dialogueEventTime && dialogueEventDone == false)
        {
            FMODDialogueInstance.start();
            dialogueEventDone = true;
            // Debug.Log("Dialogue Event Triggered");       
        }
    
    // should the playback pause while the game is paused?

    }
}