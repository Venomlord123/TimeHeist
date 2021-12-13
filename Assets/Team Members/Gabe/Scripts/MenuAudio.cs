using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{

    [HideInInspector]
    public AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void AudioEventA()
    {
        audioManager.MenuInteractA();
    }

    public void AudioEventB()
    {
        audioManager.MenuInteractB();
    }

    public void MusicStateIntroA()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 0);
    }

    public void MusicStateIntroB()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 1);
    }

    public void MusicStateMain()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", 2);
    }
}