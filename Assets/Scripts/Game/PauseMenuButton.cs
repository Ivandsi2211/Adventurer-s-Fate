using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuButton : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Color[] colores;

    public Image buttonImage;
    public float downTime = 0.1f;
    public enum buttonStates
    {
        normal = 0,
        selected,
        down
    }
    public buttonStates currentState;
    public float nextStateTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        buttonImage = GetComponent<Image>();

        if (currentState == PauseMenuButton.buttonStates.selected)
        {
            buttonImage.color = colores[1];
        }
        else if (currentState == PauseMenuButton.buttonStates.normal)
        {
            buttonImage.color = colores[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (currentState == buttonStates.normal)
            {
                buttonImage.color = colores[0];
            }
            else if (currentState == buttonStates.selected)
            {
                buttonImage.color = colores[1];
            }
            else
            {
                buttonImage.color = colores[2];
                if (nextStateTime > 0.0f)
                {
                    if (nextStateTime < Time.time)
                    {
                        // Retornar el botó a estat “no polsat”
                        nextStateTime = 0.0f;
                        currentState = PauseMenuButton.buttonStates.selected;
                        buttonImage.color = colores[1];
                        if (name == "ResumeGame_Button")
                        {
                            pauseMenu.Resume();
                        }
                        else if (name == "SaveGame_Button")
                        {
                            pauseMenu.Resume();
                            Debug.Log("SaveGame was pressed");
                        }
                        else if (name == "MainMenuGame_Button")
                        {
                            pauseMenu.Resume();
                            SceneManager.LoadScene("Main", LoadSceneMode.Single);
                        }
                        else if (name == "ExitGame_Button")
                        {
                            pauseMenu.Resume();
#if UNITY_EDITOR
                            // Application.Quit() does not work in the editor so
                            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                            UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                        }
                    }
                }
            }
        }
    }

    public void selectButton()
    {
        pauseMenu.pauseMenuButtons[pauseMenu.Posicion].currentState = PauseMenuButton.buttonStates.normal;
        if (name == "ResumeGame_Button")
        {
            pauseMenu.Posicion = 0;
        }
        else if (name == "SaveGame_Button")
        {
            pauseMenu.Posicion = 1;
        }
        else if (name == "MainMenuGame_Button")
        {
            pauseMenu.Posicion = 2;
        }
        else if (name == "ExitGame_Button")
        {
            pauseMenu.Posicion = 3;
        }
        currentState = PauseMenuButton.buttonStates.selected;
    }

    public void onClickButton()
    {
        nextStateTime = Time.time + downTime;
        currentState = PauseMenuButton.buttonStates.down;
    }

}