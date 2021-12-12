using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu Scene");
        audioManager.GameStateJournal();
    }
}