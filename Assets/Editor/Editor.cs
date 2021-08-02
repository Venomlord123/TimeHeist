using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor : UnityEditor.Editor
{
    [CustomEditor(typeof(GameManager))]
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager) target;

        if (GUILayout.Button("Start round"))
        {
            gameManager.GameStart();
        }

        if (GUILayout.Button("End round"))
        {
            gameManager.GameEnd();
        }

        if (GUILayout.Button("Switch to journal"))
        {
            gameManager.GameSwitchScene();
        }

        if (GUILayout.Button("Switch to main scene"))
        {
            gameManager.JournalSwitchScene();
        }

        if (GUILayout.Button("Reset level"))
        {
            gameManager.ResetLevel();
        }
    }
}
