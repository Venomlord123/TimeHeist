using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  //references
  
  //variables
  
  //events
  public event Action gameStartEvent;
  public event Action gameEndEvent;
  public event Action gameSwitchSceneEvent;
  public event Action journalSwitchSceneEvent;
  public event Action resetLevelEvent;

  /// <summary>
  /// TODO Tell Timer to start. Enable movement
  /// </summary>
  public void GameStart()
  {
    gameStartEvent?.Invoke();
    Debug.Log("Round started");
  }

  /// <summary>
  /// TODO player unable to move.
  /// </summary>
  public void GameEnd()
  {
    gameEndEvent?.Invoke();
    Debug.Log("Round ended");
  }

  /// <summary>
  /// TODO sceneManager and JournalMan need to subscribe
  /// </summary>
  public void GameSwitchScene()
  {
    gameSwitchSceneEvent?.Invoke();
    Debug.Log("Switch to journal");
  }

  /// <summary>
  /// TODO JournalMan and SceneMan need to subscribe
  /// </summary>
  public void JournalSwitchScene()
  {
    journalSwitchSceneEvent?.Invoke();
    Debug.Log("Switch to game");
  }

  /// <summary>
  /// TODO Timer, SceneMan, NPCMan and Player need to subscribe (object pos resets 
  /// TODO this will also remove remembered NPCs info from the player journal
  /// </summary>
  public void ResetLevel()
  {
    resetLevelEvent?.Invoke();
    Debug.Log("Reset level");
  }
}
