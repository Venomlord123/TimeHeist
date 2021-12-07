using Luke;
using ZachFrench;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public GameManager gameManager;
    public PlayerMovementTimeStop playerMovementTimeStop;

    public int gameState;

    [Header("Debugger settings")]  
    public KeyCode DebugKey;

    // [FMODUnity.EventRef]
    // public string FMODDebugEvent;
    // FMOD.Studio.EventInstance FMODDebugInstance;



    [Header("Heist Events")]
    [FMODUnity.EventRef]
    public string FMODBlackOutEvent;
    FMOD.Studio.EventInstance FMODBlackOutInstance;

    [FMODUnity.EventRef]
    public string FMODFireAlarmEvent;
    FMOD.Studio.EventInstance FMODFireAlarmInstance;

    // [FMODUnity.EventRef]
    // public string FMODChatterEvent;
    // FMOD.Studio.EventInstance FMODChatterInstance;



    // [Header("Music Event")]
    // [FMODUnity.EventRef]
    // public string FMODMusicEvent;
    // FMOD.Studio.EventInstance FMODMusicInstance;

    // public KeyCode GreenRoomKey;
    // public KeyCode RedRoomKey;
    // public KeyCode YellowRoomKey;
    // public KeyCode BlueRoomKey;
    public KeyCode GameStateJournalKey;
    public KeyCode GameStatePlayKey;



    [Header("UI Events")]
    [FMODUnity.EventRef]
    public string FMODSuspectAddedEvent;
    FMOD.Studio.EventInstance FMODSuspectAddedInstance;

    [FMODUnity.EventRef]
    public string FMODPauseSFXEvent;
    FMOD.Studio.EventInstance FMODPauseSFXInstance;

    [FMODUnity.EventRef]
    public string FMODUnpauseSFXEvent;
    FMOD.Studio.EventInstance FMODUnpauseSFXInstance;



    // Initialization and testing

    void Start()
    {
        gameState = 1;
        FMODPauseSFXInstance = FMODUnity.RuntimeManager.CreateInstance (FMODPauseSFXEvent);
        FMODUnpauseSFXInstance = FMODUnity.RuntimeManager.CreateInstance (FMODUnpauseSFXEvent);
    }

    void Update ()
    {
        if (Input.GetKeyDown(DebugKey)) {
            DebugEvent();
        }
        
        if (gameState == 1)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Speed", playerMovementTimeStop.playerVelocity);
        }
        
        if (Input.GetKeyDown(GameStateJournalKey)) {
            GameStateJournal();
        }

        if (Input.GetKeyDown(GameStatePlayKey)) {
            GameStatePlay();
        }

        // if (Input.GetKeyDown(GreenRoomKey)) {
        //     AreaGreenRoom();
        // }

        // if (Input.GetKeyDown(RedRoomKey)) {
        //     AreaRedRoom();
        // }
        
        // if (Input.GetKeyDown(YellowRoomKey)) {
        //     AreaYellowRoom();
        // }

        // if (Input.GetKeyDown(BlueRoomKey)) {
        //     AreaBlueRoom();
        // }
    }

    public void DebugEvent()
    {
        Debug.Log("Gamestate is " + gameState);
        // gameManager.GameSwitchScene();
    }



    // Playstate Events

    public void PauseGame()
    {
        // Debug.Log("Menu Pause Audio Event");
        FMODPauseSFXInstance.start();
        FMODUnity.RuntimeManager.GetBus("bus:/MUS BUS").setPaused(true);
        FMODUnity.RuntimeManager.GetBus("bus:/DX BUS").setPaused(true);
    }

    public void UnpauseGame()
    {
        // Debug.Log("Game Resume Audio Event");
        FMODUnpauseSFXInstance.start(); 
        FMODUnity.RuntimeManager.GetBus("bus:/MUS BUS").setPaused(false);
        FMODUnity.RuntimeManager.GetBus("bus:/DX BUS").setPaused(false);
    }

    public void GameStateJournal()
    {
        gameState = 0;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameMode", 0);
        FMODUnity.RuntimeManager.GetBus("bus:/ATMOS BUS").setPaused(true);
        FMODUnity.RuntimeManager.GetBus("bus:/DX BUS").setPaused(true);
        Debug.Log("GameStateJournal called. Gamestate is " + gameState);
        // do other audio stuff here
    }

    public void GameStatePlay()
    {
        gameState = 1;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameMode", 1);
        FMODUnity.RuntimeManager.GetBus("bus:/ATMOS BUS").setPaused(false);
        FMODUnity.RuntimeManager.GetBus("bus:/DX BUS").setPaused(false);
        Debug.Log("GameStatePlay called. Gamestate is " + gameState);
        // do other audio stuff here
    }   



    // UI events

    public void SuspectAdded()
    {
        // Debug.Log("Suspect Added Audio Event");
        FMODSuspectAddedInstance = FMODUnity.RuntimeManager.CreateInstance (FMODSuspectAddedEvent);
        FMODSuspectAddedInstance.start ();
    }



    // Location Events

    public void AreaGreenRoom()
    {
        Debug.Log("Green Room Event Triggered");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RED", 0);
    }

    public void AreaRedRoom()
    {
        Debug.Log("Red Room Event Triggered");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RED", 1);
        // fade out kitchen noise (basically just set that parameter to 1)
    }

    public void AreaYellowRoom()
    {
        Debug.Log("Yellow Room Event Triggered");
        // FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RED", 0);
    }

    public void AreaBlueRoom()
    {
        Debug.Log("Blue Room Event Triggered");
        // FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RED", 0);
    }

    public void AreaKitchen()
    {
        Debug.Log("Kitchen Event Triggered");
        // Bring down crowd atmos, fade in kitchen noise
    }



    // Heist Events

    public void BlackOut()
    {
        Debug.Log("Blackout audio event triggered");
        FMODBlackOutInstance = FMODUnity.RuntimeManager.CreateInstance (FMODBlackOutEvent);
        FMODBlackOutInstance.start ();
        // transition to music buildup part 2
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Concern", 1);
        // wait until lights come back on and fade halfway between calm and concerned
    }

    public void FireAlarm()
    {
        Debug.Log("Fire Alarm audio event triggered");
        FMODFireAlarmInstance = FMODUnity.RuntimeManager.CreateInstance (FMODFireAlarmEvent);
        FMODFireAlarmInstance.start ();
        // transition to buildup part 3
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Concern", 2);
        // make chatter atmos into fear
    }
}