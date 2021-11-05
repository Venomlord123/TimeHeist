using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
   public GameManager gameManager;
   public AudioSource audioSource;

   private void OnEnable()
   {
      gameManager.timer.FireAlarmEvent += FireAlarm;
   }

   private void OnDisable()
   {
      gameManager.timer.FireAlarmEvent -= FireAlarm;
   }

   private void FireAlarm()
   {
      audioSource.Play();
   }
}
