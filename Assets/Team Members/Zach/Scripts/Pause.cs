using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    public void PauseGame()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void UnpauseGame()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}