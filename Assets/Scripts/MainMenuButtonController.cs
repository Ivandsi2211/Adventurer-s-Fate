using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonController : MonoBehaviour
{
    public GameStates stateManager = null;
    public MainMenuButton[] mainMenuButtons;
    public int Posicion;
    private bool pressMovementButton;
    private bool notPressedCancelButton;

    // Use this for initialization
    void Start()
    {
        Posicion = 0;
        pressMovementButton = false;
        notPressedCancelButton = true;
        mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressMovementButton)
        {
            if (Input.GetAxis("Vertical") == -1)
            {
                pressMovementButton = true;
                mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
                Posicion++;
                if (Posicion > mainMenuButtons.Length - 1)
                {
                    Posicion = 0;
                }
                mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
            }
            else if (Input.GetAxis("Vertical") == 1)
            {
                pressMovementButton = true;
                mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
                Posicion--;
                if (Posicion < 0)
                {
                    Posicion = mainMenuButtons.Length - 1;
                }
                mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
            }
        }
        else
        {
            if (Input.GetAxis("Vertical") < 1 && Input.GetAxis("Vertical") > -1)
            {
                pressMovementButton = false;
            }
        }

        if (Input.GetAxis("Confirmation Button") > 0)
        {
            mainMenuButtons[Posicion].nextStateTime = Time.time + mainMenuButtons[Posicion].downTime;
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.down;
        }

        if (!notPressedCancelButton)
        {
            if (Input.GetAxis("Cancel/Menu Button") < 1)
            {
                notPressedCancelButton = true;
                stateManager.changeToStartMenu();
            }
        }
        else
        {
            if (Input.GetAxis("Cancel/Menu Button") == 1)
            {
                notPressedCancelButton = false;
            }
        }

    }
}
