using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public float downTime = 0.1f;
    public GameStates stateManager = null;
    public GameObject fading_Label;
    public float interval;
    public enum menuStates
    {
        actived = 0,
        deactived
    }
    public menuStates currentState;

    public Animator anim;

    void Start()
    {
        currentState = menuStates.actived;
    }

    void OnEnable()
    {
        Debug.Log("Enabled");
        ManageAnimation();
        anim.SetBool("active", true);
    }

    IEnumerator ManageAnimation()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void OnMouseDown()
    {
        if (currentState == StartMenuController.menuStates.actived)
        {
            currentState = StartMenuController.menuStates.deactived;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel/Menu Button"))
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
        else if (!Input.GetButtonDown("Cancel/Menu Button") &&
                !Input.GetButton("Cancel/Menu Button") &&
                !Input.GetButtonUp("Cancel/Menu Button"))
        {
            if (Input.anyKey && currentState == StartMenuController.menuStates.actived)
            {
                currentState = StartMenuController.menuStates.deactived;
            }
            else if (!Input.anyKey && currentState == StartMenuController.menuStates.deactived)
            {
                // Retornar el botó a estat “no polsat”
                anim.SetBool("active", false);
                currentState = StartMenuController.menuStates.actived;
                stateManager.changeToMainMenu();
            }
        }
    }
}
