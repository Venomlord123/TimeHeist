using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioSettings audioSettings;

    public enum AssignedBus
        {
            SFXVolumeLevel,
            MusicVolumeLevel,
            DialogueVolumeLevel,
            MasterVolumeLevel,
        };

    public AssignedBus BusSelect;

    public void SetLevel (float sliderValue)
    {
        if (BusSelect == AssignedBus.MasterVolumeLevel)
            audioSettings.MasterVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.SFXVolumeLevel)
            audioSettings.SFXVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.MusicVolumeLevel)
            audioSettings.MusicVolumeLevel(sliderValue);
        else if (BusSelect == AssignedBus.DialogueVolumeLevel)
            audioSettings.DialogueVolumeLevel(sliderValue);            
    }
}
