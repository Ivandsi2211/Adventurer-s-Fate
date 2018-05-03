using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.S))
        {
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
            Posicion++;

            if (Posicion < 0)
            {
                Posicion = mainMenuButtons.Length - 1;
            }

            if (Posicion > mainMenuButtons.Length - 1)
            {
                Posicion = 0;
            }

            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.normal;
            Posicion--;

            if (Posicion < 0)
            {
                Posicion = mainMenuButtons.Length - 1;
            }

            if (Posicion > mainMenuButtons.Length - 1)
            {
                Posicion = 0;
            }

            mainMenuButtons[Posicion].currentState = MainMenuButton.buttonStates.selected;
        }
    }
}
