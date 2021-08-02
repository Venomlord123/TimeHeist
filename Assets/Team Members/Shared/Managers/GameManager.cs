using System;
using System.Collections;
using System.Collections.Generic;
using Luke;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  //references
  public Timer timer;
  
  //variables
  //TODO roundCounter++ when new round starts
  public int roundCounter;
  
  //events
  public event Action GameStartEvent;
  public event Action GamePauseEvent;
  public event Action GameEndEvent;
  public event Action GameSwitchSceneEvent;
  public event Action JournalSwitchSceneEvent;
  public event Action ResetLevelEvent;

  private void OnEnable()
  {
    timer.TimerEndEvent += GameEnd;
  }

  private void OnDisable()
  {
    timer.TimerEndEvent -= GameEnd;
  }

  /// <summary>
  /// TODO Enable movement this will also include continuing the game from pause menu
  /// </summary>
  public void GameStart()
  {
    GameStartEvent?.Invoke();
    Debug.Log("Round started");
  }

  /// <summary>
  /// TODO Pause menu pop up
  /// </summary>
  public void GamePause()
  {
    GamePauseEvent?.Invoke();
    Debug.Log("Paused game");
  }

  /// <summary>
  /// TODO player unable to move.
  /// </summary>
  public void GameEnd()
  {
    GameEndEvent?.Invoke();
    Debug.Log("Round ended");
  }

  /// <summary>
  /// TODO sceneManager and JournalMan need to subscribe
  /// </summary>
  public void GameSwitchScene()
  {
    GameSwitchSceneEvent?.Invoke();
    Debug.Log("Switch to journal");
  }

  /// <summary>
  /// TODO JournalMan and SceneMan need to subscribe
  /// </summary>
  public void JournalSwitchScene()
  {
    JournalSwitchSceneEvent?.Invoke();
    Debug.Log("Switch to game");
  }

  /// <summary>
  /// TODO Timer, SceneMan, NPCMan and Player need to subscribe (object pos resets 
  /// TODO this will also remove remembered NPCs info from the player journal
  /// </summary>
  public void ResetLevel()
  {
    ResetLevelEvent?.Invoke();
    Debug.Log("Reset level");
  }
}
