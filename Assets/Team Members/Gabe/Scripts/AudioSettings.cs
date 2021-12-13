using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    FMOD.Studio.EventInstance SFXVolumeTestEvent; // power off?
    FMOD.Studio.EventInstance MusicVolumeTestEvent; // brass stab
    FMOD.Studio.EventInstance DialogueVolumeTestEvent; //"Hey!"

    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Dialogue;
    FMOD.Studio.Bus Master;

    // [SerializeField]
    // [Range(0f, 1f)]
    public float SFXVolume;

    // [SerializeField]
    // [Range(0f, 1f)]
    public float MusicVolume;

    // [SerializeField]
    // [Range(0f, 1f)]
    public float DialogueVolume;

    // [SerializeField]
    // [Range(0f, 1f)]
    public float MasterVolume;



    void Start()
    {   
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/SFX BUS");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/MUS BUS");
        Dialogue = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER/DX BUS");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/PREMASTER");
    }

    void Update()
    {
        SFX.setVolume(SFXVolume);
        Music.setVolume (MusicVolume);
        Dialogue.setVolume(DialogueVolume);
        Master.setVolume(MasterVolume);
    }

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
                
        // FMOD.Studio.PLAYBACK_STATE PbState; // will I need to make more of these for the other sliders?
        // SFXVolumeTestEvent.getPlaybackState (out PbState);
        // if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
        // {
        //     SFXVolumeTestEvent.start ();
        // }
    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void DialogueVolumeLevel (float newDialogueVolume)
    {
        DialogueVolume = newDialogueVolume;
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

}
