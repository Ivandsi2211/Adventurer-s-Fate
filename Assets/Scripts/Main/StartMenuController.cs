using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public float downTime = 0.1f;
    public GameStates stateManager = null;
    public GameObject flashing_Label;
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
        InvokeRepeating("FlashLabel", 0, interval);
    }

    void FlashLabel()
    {
        if (flashing_Label.GetComponent<SpriteRenderer>().enabled)
        {
            flashing_Label.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            flashing_Label.GetComponent<SpriteRenderer>().enabled = true;
        }
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
            if (currentState == StartMenuController.menuStates.actived)
            {
                nextStateTime = Time.time + downTime;
                currentState = StartMenuController.menuStates.deactived;
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
                stateManager.changeToMainMenu();
            }
        }
    }
}
