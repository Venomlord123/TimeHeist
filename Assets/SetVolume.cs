using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    private AudioManager audioManager;
    public Slider slider;

    public enum AssignedBus
        {
            SFXVolumeLevel,
            MusicVolumeLevel,
            DialogueVolumeLevel,
            MasterVolumeLevel,
        };

    public AssignedBus BusSelect;



    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (BusSelect == AssignedBus.MasterVolumeLevel)
            slider.value = audioManager.MasterVolume;
        else if (BusSelect == AssignedBus.SFXVolumeLevel)
            slider.value = audioManager.SFXVolume;
        else if (BusSelect == AssignedBus.MusicVolumeLevel)
            slider.value = audioManager.MusicVolume;
        else if (BusSelect == AssignedBus.DialogueVolumeLevel)
            slider.value = audioManager.DialogueVolume;
    }



    public void SetLevel (float sliderValue)
    {
        if (BusSelect == AssignedBus.MasterVolumeLevel)
            audioManager.MasterVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.SFXVolumeLevel)
            audioManager.SFXVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.MusicVolumeLevel)
            audioManager.MusicVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.DialogueVolumeLevel)
            audioManager.DialogueVolumeLevel(sliderValue);            
    }
}
