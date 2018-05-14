using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool menuIsActive;
    public static bool gameIsPaused;
    public GameObject pauseMenu;

    // Use this for initialization
    void Start()
    {
        gameIsPaused = false;
        menuIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Cancel/Menu Button") && menuIsActive)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void ExitToDesktopButton()
    {
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
           Application.Quit();
        #endif
    }
}
