using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonController : MonoBehaviour
{
    public MainMenuButton[] mainMenuButtons;
    public int Posicion;

    // Use this for initialization
    void Start()
    {
        Posicion = 0;
        mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
            Posicion++;
            if (Posicion > mainMenuButtons.Length - 1)
            {
                Posicion = 0;
            }
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
            Posicion--;
            if (Posicion < 0)
            {
                Posicion = mainMenuButtons.Length - 1;
            }
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            mainMenuButtons[Posicion].nextStateTime = Time.time + mainMenuButtons[Posicion].downTime;
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.down;
        }
    }
}
