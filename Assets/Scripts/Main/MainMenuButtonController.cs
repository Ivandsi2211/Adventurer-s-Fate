using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuButtonController : MonoBehaviour
{
    public GameStates stateManager = null;
    public MainMenuButton[] mainMenuButtons;
    public int Posicion;
    private bool pressMovementButton;
    private bool pressCancelButton;
    private bool pressConfirmationButton;

    // Use this for initialization
    void Start()
    {
        Posicion = 0;
        pressMovementButton = false;
        pressCancelButton = false;
        pressConfirmationButton = false;
        mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressMovementButton && !pressConfirmationButton)
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

        if (!pressCancelButton)
        {
            if (Input.GetButtonDown("Confirmation Button"))
            {
                mainMenuButtons[Posicion].nextStateTime = Time.time + mainMenuButtons[Posicion].downTime;
                mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.down;
                pressConfirmationButton = true;
            }
            else if (Input.GetButtonUp("Confirmation Button"))
            {
                pressConfirmationButton = false;
            }
        }

        if (!pressConfirmationButton)
        {
            if (Input.GetButtonDown("Cancel/Menu Button"))
            {
                pressCancelButton = true;
            }
            else if (Input.GetButtonUp("Cancel/Menu Button"))
            {
                pressCancelButton = false;
                stateManager.changeToStartMenu();
            }
        }

    }
}
