using UnityEngine;
using System.Collections;
public class GameStates : MonoBehaviour
{
    public GameObject StartMenuContainer;
    public GameObject MainMenuContainer;
    public enum displayStates
    {
        StartMenuContainer = 0,
        MainMenuContainer
    }    void Start()
    {
        Vector2 cameraSize = new Vector2(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);
        StartMenuContainer.GetComponent<BoxCollider2D>().size = cameraSize;
        changeDisplayState(displayStates.StartMenuContainer);

    }
    public void changeDisplayState(displayStates newState)
    {
        StartMenuContainer.SetActive(false);
        MainMenuContainer.SetActive(false);
        switch (newState)
        {
            case displayStates.StartMenuContainer:
                StartMenuContainer.SetActive(true);
                break;
            case displayStates.MainMenuContainer:
                MainMenuContainer.SetActive(true);
                break;
        }
    }
    public void changeToMainMenu()
    {
        changeDisplayState(displayStates.MainMenuContainer);
    }

    public void changeToStartMenu()
    {
        changeDisplayState(displayStates.StartMenuContainer);
    }
}