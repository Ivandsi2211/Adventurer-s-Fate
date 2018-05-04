using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public MainMenuButtonController mainMenuButtonController;
    public Color[] colores;
    public SpriteRenderer buttonSprite;
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
        currentState = buttonStates.normal;
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.color = colores[0];
    }

    void OnMouseOver()
    {
        if (currentState == MainMenuButton.buttonStates.normal)
        {
            mainMenuButtonController.mainMenuButtons[mainMenuButtonController.Posicion].currentState = MainMenuButton.buttonStates.normal;
            if (name == "NewGame_Button")
            {
                mainMenuButtonController.Posicion = 0;
            }
            else if (name == "LoadGame_Button")
            {
                mainMenuButtonController.Posicion = 1;
            }
            else if (name == "ExitGame_Button")
            {
                mainMenuButtonController.Posicion = 2;
            }
            currentState = MainMenuButton.buttonStates.selected;
        }
    }

    void OnMouseDown()
    {
        nextStateTime = Time.time + downTime;
        currentState = MainMenuButton.buttonStates.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == buttonStates.normal)
        {
            buttonSprite.color = colores[0];
        }
        else if (currentState == buttonStates.selected)
        {
            buttonSprite.color = colores[1];
        }
        else
        {
            buttonSprite.color = colores[2];
            if (nextStateTime > 0.0f)
            {
                if (nextStateTime < Time.time)
                {
                    // Retornar el botó a estat “no polsat”
                    nextStateTime = 0.0f;
                    currentState = MainMenuButton.buttonStates.selected;
                    buttonSprite.color = colores[1];
                    if (name == "NewGame_Button")
                    {
                        SceneManager.LoadScene("Game", LoadSceneMode.Single);
                    }
                    else if (name == "LoadGame_Button")
                    {
                        Debug.Log("LoadGame was pressed");
                    }
                    else if (name == "ExitGame_Button")
                    {
                        Debug.Log("Se cerrará el juego");
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
