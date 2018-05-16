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
    private float nextStateTime = 0.0f;
    void Start()
    {
        currentState = menuStates.actived;
    }

    void OnEnable()
    {
        StartCoroutine(ManageAnimation());
    }

    IEnumerator ManageAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        fading_Label.GetComponent<Animator>().Play("Start_Fading");
    }

        void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == StartMenuController.menuStates.actived)
        {
            nextStateTime = Time.time + downTime;
            currentState = StartMenuController.menuStates.deactived;
        }
    }
    void Update()
    {
        if (Input.anyKey)
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
                if (currentState == StartMenuController.menuStates.actived)
                {
                    nextStateTime = Time.time + downTime;
                    currentState = StartMenuController.menuStates.deactived;
                }
            }
        }
        if (nextStateTime > 0.0f)
        {
            if (nextStateTime < Time.time &&
                currentState == StartMenuController.menuStates.deactived)
            {
                // Retornar el botó a estat “no polsat”
                nextStateTime = 0.0f;
                currentState = StartMenuController.menuStates.actived;
                fading_Label.GetComponent<Animator>().Play("Start_Idle");
                stateManager.changeToMainMenu();
            }
        }
    }
}
