using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {
    public float downTime = 0.1f;
    public GameStates stateManager = null;
    public GameObject flashing_Label;
    public float interval;
    private enum menuStates
    {
        actived = 0,
        deactived
    }
    private menuStates currentState = menuStates.actived;
    private float nextStateTime = 0.0f;
    void Start()
    {
        InvokeRepeating("FlashLabel", 0, interval);
    }

    void FlashLabel()
    {
        if (flashing_Label.activeSelf)
            flashing_Label.SetActive(false);
        else
            flashing_Label.SetActive(true);
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
        if (Input.anyKey){
            if (nextStateTime == 0.0f
                     &&
                     currentState == StartMenuController.menuStates.actived)
            {
                nextStateTime = Time.time + downTime;
                currentState = StartMenuController.menuStates.deactived;
            }
        }
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
