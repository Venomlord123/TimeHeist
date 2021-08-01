using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public event Action gameStartEvent;

  public event Action gameEndEvent;

  public event Action gameSwitchSceneEvent;

  public event Action journalSwitchSceneEvent;

  public event Action resetLevelEvent;

}
