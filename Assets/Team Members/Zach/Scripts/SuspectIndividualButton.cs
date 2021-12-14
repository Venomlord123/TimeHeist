using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectIndividualButton : MonoBehaviour
{
   public NPCInformation npcInformation;
   public event Action<NPCInformation> OnButtonPressDetailsEvent;
   public event Action<NPCInformation> OnButtonPressAccuseEvent;

   public GameObject circle;
   public GameObject cross;

   public void ExpandDetails()
   {
      OnButtonPressDetailsEvent?.Invoke(npcInformation);
   }

   public void Accuse()
   {
      OnButtonPressAccuseEvent?.Invoke(npcInformation);
   }

   public void Circle()
   {
      if (circle.activeInHierarchy != true)
      {
         circle.SetActive(true);
      }
      else
      {
         circle.SetActive(false);
      }
   }
   

   public void Cross()
   {
      if (cross.activeInHierarchy != true)
      {
         cross.SetActive(true);
      }
      else
      {
         cross.SetActive(false);
      }
   }
}
