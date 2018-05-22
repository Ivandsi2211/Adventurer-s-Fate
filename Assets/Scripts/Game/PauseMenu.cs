using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool menuIsActive;
    public static bool gameIsPaused;
    public GameObject pauseMenu;
    public MainMenuButton[] pauseMenuButtons;

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
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }

    public void ResumeGame_Button()
    {
        Resume();
    }

    public void SaveGame_Button()
    {
        Resume();
        Debug.Log("SaveGame was pressed");
    }

    public void MainMenuGame_Button()
    {
        Resume();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ExitGame_Button()
    {
        Resume();
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
           Application.Quit();
#endif
    }
}
