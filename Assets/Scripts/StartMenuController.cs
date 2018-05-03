using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {
    public GameObject activateSprite;
    public float downTime = 0.1f;
    public GameStates stateManager = null;
    private enum menuStates
    {
        actived = 0,
        deactived
    }
    private menuStates currentState = menuStates.actived;
    private float nextStateTime = 0.0f;
    void Start()
    {
        activateSprite.SetActive(true);
    }
    void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == StartMenuController.menuStates.actived)
        {
            nextStateTime = Time.time + downTime;
            currentState = StartMenuController.menuStates.deactived;
            stateManager.changeToMainMenu();
            activateSprite.SetActive(false);
        }
    }
    void Update()
    {
        if (nextStateTime > 0.0f)
        {
            if (nextStateTime < Time.time)
            {
                // Retornar el botó a estat “no polsat”
                nextStateTime = 0.0f;
                stateManager.changeToMainMenu();
            }
        }
    }
}
