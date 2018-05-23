using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool menuIsActive;
    public static bool gameIsPaused;
    public GameObject pauseMenu;

    public PauseMenuButton[] pauseMenuButtons;
    public int Posicion;
    private bool pressMovementButton;
    private bool pressCancelButton;
    private bool pressConfirmationButton;

    // Use this for initialization
    void Start()
    {
        gameIsPaused = false;
        menuIsActive = true;

        Posicion = 0;
        pressMovementButton = false;
        pressCancelButton = false;
        pressConfirmationButton = false;
        pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.selected;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuIsActive)
        {
            if (!pressMovementButton && !pressConfirmationButton)
            {
                if (Input.GetAxis("Vertical") == -1)
                {
                    pressMovementButton = true;
                    pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.normal;
                    Posicion++;
                    if (Posicion > pauseMenuButtons.Length - 1)
                    {
                        Posicion = 0;
                    }
                    pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.selected;
                }
                else if (Input.GetAxis("Vertical") == 1)
                {
                    pressMovementButton = true;
                    pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.normal;
                    Posicion--;
                    if (Posicion < 0)
                    {
                        Posicion = pauseMenuButtons.Length - 1;
                    }
                    pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.selected;
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
                    pauseMenuButtons[Posicion].nextStateTime = Time.time + pauseMenuButtons[Posicion].downTime;
                    pauseMenuButtons[Posicion].currentState = PauseMenuButton.buttonStates.down;
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
                    if (gameIsPaused)
                    {
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }
                }
                else
                {
                    pressCancelButton = false;
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }
}
