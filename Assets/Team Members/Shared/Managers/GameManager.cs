using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
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
  }

  /// <summary>
  /// TODO player unable to move.
  /// </summary>
  public void GameEnd()
  {
    gameEndEvent?.Invoke();
  }

  /// <summary>
  /// TODO sceneManager and JournalMan need to subscribe
  /// </summary>
  public void GameSwitchScene()
  {
    gameSwitchSceneEvent?.Invoke();
  }

  /// <summary>
  /// TODO JournalMan and SceneMan need to subscribe
  /// </summary>
  public void JournalSwitchScene()
  {
    journalSwitchSceneEvent?.Invoke();
  }

  /// <summary>
  /// TODO Timer, SceneMan, NPCMan and Player need to subscribe
  /// </summary>
  public void ResetLevel()
  {
    resetLevelEvent?.Invoke();
  }
}
