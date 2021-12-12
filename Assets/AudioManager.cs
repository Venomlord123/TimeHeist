using Luke;
using ZachFrench;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    private PlayerMovementTimeStop playerMovementTimeStop;
    // public AudioSettings audioSettings;

    [HideInInspector]
    public int gameState = 0;
    [HideInInspector]
    int accusationCounter = 0;
    public static AudioManager instance;
    

    public KeyCode DebugKey;

    [FMODUnity.EventRef]
    private string FMODVolumeTestSFXEvent = "event:/SFX/VolumeTests/SFX";
    FMOD.Studio.EventInstance FMODVolumeTestSFXInstance;

    [FMODUnity.EventRef]
    private string FMODVolumeTestMusicEvent = "event:/SFX/VolumeTests/Music";
    FMOD.Studio.EventInstance FMODVolumeTestMusicInstance; 

    [FMODUnity.EventRef]
    private string FMODVolumeTestDialogueEvent = "event:/SFX/VolumeTests/Dialogue";
    FMOD.Studio.EventInstance FMODVolumeTestDialogueInstance;

    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Dialogue;
    FMOD.Studio.Bus Master;


    [Header("Audio settings")]   
    [Range(0f, 1f)]
    public float SFXVolume = 0.5f;

    [Range(0f, 1f)]
    public float MusicVolume = 0.5f;

    [Range(0f, 1f)]
    public float DialogueVolume = 0.5f;
    
    [Range(0f, 1f)]
    public float MasterVolume = 1f;


    [Header("Music Event")]
    [FMODUnity.EventRef]
    private string FMODMusicEvent = "event:/Music/Music";
    FMOD.Studio.EventInstance FMODMusicInstance;

        // public KeyCode GreenRoomKey;
        // public KeyCode RedRoomKey;
        // public KeyCode YellowRoomKey;
        // public KeyCode BlueRoomKey;



    [Header("UI Events")]
    [FMODUnity.EventRef]
    private string FMODSuspectAddedEvent = "event:/SFX/Menu/SuspectAdded";
    FMOD.Studio.EventInstance FMODSuspectAddedInstance;

    [FMODUnity.EventRef]
    private string FMODPauseSFXEvent = "event:/SFX/Menu/Pause";
    FMOD.Studio.EventInstance FMODPauseSFXInstance;

    [FMODUnity.EventRef]
    private string FMODUnpauseSFXEvent = "event:/SFX/Menu/Unpause";
    FMOD.Studio.EventInstance FMODUnpauseSFXInstance;

    [FMODUnity.EventRef]
    private string FMODOpeningDialogueEvent = "event:/SFX/Dialogue/Opening";
    FMOD.Studio.EventInstance FMODOpeningDialogueInstance;

    [FMODUnity.EventRef]
    private string FMODClosingDialogueEvent = "event:/SFX/Dialogue/Closing";
    FMOD.Studio.EventInstance FMODClosingDialogueInstance;


    [Header("Heist Events")]
    [FMODUnity.EventRef]
    private string FMODBlackOutEvent = "event:/SFX/Heist/BlackOut";
    FMOD.Studio.EventInstance FMODBlackOutInstance;

    [FMODUnity.EventRef]
    private string FMODFireAlarmEvent = "event:/SFX/Heist/FireAlarm";
    FMOD.Studio.EventInstance FMODFireAlarmInstance;

    // void Awake()
    // {
    //     if(instance == null)
    //     {
    //         instance = this;
    //         return;
    //     }
    //     Destroy(this.gameObject);
    // }

    // void Awake()
    // {
    //     if(GameObject.Find ("AudioManagerObject")){
    //         Debug.Log ("Exists");
    //     }
    // }

    private void Awake() 
    {
        if (instance != null) { 
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        // Debug.Log("Instance check: " + instance);
    }

    void Start()
    {
        FMODPauseSFXInstance = FMODUnity.RuntimeManager.CreateInstance (FMODPauseSFXEvent);
        FMODUnpauseSFXInstance = FMODUnity.RuntimeManager.CreateInstance (FMODUnpauseSFXEvent);
        FMODOpeningDialogueInstance = FMODUnity.RuntimeManager.CreateInstance (FMODOpeningDialogueEvent);
        FMODClosingDialogueInstance = FMODUnity.RuntimeManager.CreateInstance (FMODClosingDialogueEvent);

        // init audio settings menu sounds
        FMODVolumeTestSFXInstance = FMODUnity.RuntimeManager.CreateInstance (FMODVolumeTestSFXEvent);
        FMODVolumeTestMusicInstance = FMODUnity.RuntimeManager.CreateInstance (FMODVolumeTestMusicEvent);
        FMODVolumeTestDialogueInstance = FMODUnity.RuntimeManager.CreateInstance (FMODVolumeTestDialogueEvent);

        FMODMusicInstance = FMODUnity.RuntimeManager.CreateInstance (FMODMusicEvent);
        FMODMusicInstance.start(); 
        
        DontDestroyOnLoad(this.gameObject);

        SFX = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/MUS BUS");
        Dialogue = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER");
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

        SFX.setVolume(SFXVolume);
        Music.setVolume (MusicVolume);
        Dialogue.setVolume(DialogueVolume);
        Master.setVolume(MasterVolume);

        // if (Input.GetKeyDown(GreenRoomKey)) {
        //     IncrementAccusationCounter();
        //     // AreaGreenRoom();
        // }

        // if (Input.GetKeyDown(RedRoomKey)) {
        //     ResetAccusations();
        //     // AreaRedRoom();
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
    }



    // Playstate functions

    public void GameStatePlay()
    {
        gameState = 1;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 2);

        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS/ATMOS BUS").setPaused(false);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS/Heist SFX").setPaused(false);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS").setPaused(false);

        playerMovementTimeStop = FindObjectOfType<PlayerMovementTimeStop>();

        Debug.Log("GameStatePlay called. Gamestate is " + gameState);
    }   

    public void GameStateJournal()
    {
        gameState = 0;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Concern", 0);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 0);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicRedness", 1);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicBlueness", 1);

        FMODMusicInstance.setPitch(1f);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS/ATMOS BUS").setPaused(true);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS/Heist SFX").setPaused(true);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS").setPaused(true);
        

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Speed", 1);

        Debug.Log("GameStateJournal called. Gamestate is " + gameState);
    }

    public void PauseGame()
    {
        gameState = 0;

        MenuInteractA();
        FMODMusicInstance.setPitch(0.5f);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS").setPaused(true);
        
        Debug.Log("Menu Pause Audio Event");
    }

    public void UnpauseGame()
    {
        gameState = 1;

        MenuInteractB();
        FMODMusicInstance.setPitch(1f);
        FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS").setPaused(false);

        Debug.Log("Game Resume Audio Event");
    }



    // UI functions

    public void MenuInteractA()
    {
        FMODPauseSFXInstance.start(); 
    }

    public void MenuInteractB()
    {
        FMODUnpauseSFXInstance.start(); 
    }

    public void SuspectAdded()
    {
        if (gameState == 1)
        {
            FMODSuspectAddedInstance = FMODUnity.RuntimeManager.CreateInstance (FMODSuspectAddedEvent); 

            FMODSuspectAddedInstance.start ();

            // Debug.Log("Suspect Added Audio Event");
        }
    }

    public void IncrementAccusationCounter()
    {
        if (accusationCounter >= 1) {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 1);
            accusationCounter ++;
        } else {
            accusationCounter ++;
        }
        Debug.Log("Accusation counter is: " + accusationCounter);
    }

    public void ResetAccusations()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 0);
        Debug.Log("Accusation counter reset.");
        accusationCounter = 0;
    }


    // audio settings events

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
        
        FMOD.Studio.PLAYBACK_STATE PbState;
        FMODVolumeTestSFXInstance.getPlaybackState (out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
        {
            FMODVolumeTestSFXInstance.start ();
        }
    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
        
        FMOD.Studio.PLAYBACK_STATE PbState;
        FMODVolumeTestMusicInstance.getPlaybackState (out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
        {
            FMODVolumeTestMusicInstance.start ();
        }
    }

    public void DialogueVolumeLevel (float newDialogueVolume)
    {
        DialogueVolume = newDialogueVolume;
        
        FMOD.Studio.PLAYBACK_STATE PbState;
        FMODVolumeTestDialogueInstance.getPlaybackState (out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
        {
            FMODVolumeTestDialogueInstance.start ();
        }
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }



    // Heist Events

    public void BlackOut()
    {
        FMODBlackOutInstance = FMODUnity.RuntimeManager.CreateInstance (FMODBlackOutEvent);
        FMODBlackOutInstance.start();

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Concern", 1);

        // TODO add slight lowpass to EQ with concern

        // Debug.Log("Blackout audio event triggered");
    }

    public void FireAlarm()
    {
        FMODFireAlarmInstance = FMODUnity.RuntimeManager.CreateInstance (FMODFireAlarmEvent);
        FMODFireAlarmInstance.start();

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Concern", 1);
        // TODO add slight lowpass to EQ with concern        

        // Debug.Log("Fire Alarm audio event triggered");
    }
}