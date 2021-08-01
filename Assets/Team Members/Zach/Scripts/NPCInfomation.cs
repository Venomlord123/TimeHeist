using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;
using ZachFrench;

public class NPCInfomation : MonoBehaviour
{
    public NPCBase npcBase;
    public PlayerModel playerModel;
    public string suspectName;
    public List<string> locations;
    public List<string> conversations;

    public void Awake()
    {
        playerModel = GetComponentInParent<PlayerModel>();
    }

    private void OnEnable()
    {
        playerModel.writingEvent += WritingEvent;
    }

    private void OnDisable()
    {
        playerModel.writingEvent -= WritingEvent;
    }

    private void WritingEvent(NPCBase obj)
    {
        obj.npcName = suspectName;
        
    }
}
