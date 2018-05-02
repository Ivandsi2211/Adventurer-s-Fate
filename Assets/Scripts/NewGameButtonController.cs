using UnityEngine;
using System.Collections;
public class NewGameButtonController : MonoBehaviour
{
    public GameObject upSprite;
    public GameObject downSprite;
    public float downTime = 0.1f;
    private enum buttonStates
    {
        up = 0,
        down
    }
    private buttonStates currentState = buttonStates.up;
    private float nextStateTime = 0.0f;
    void Start()
    {
        upSprite.SetActive(true);        downSprite.SetActive(false);
    }
    void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == NewGameButtonController.buttonStates.up)
        {
            nextStateTime = Time.time + downTime;
            upSprite.SetActive(false);
            downSprite.SetActive(true);
            currentState = NewGameButtonController.buttonStates.down;
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
                upSprite.SetActive(true);
                downSprite.SetActive(false);
                currentState = NewGameButtonController.buttonStates.up;
                // Començar el joc
                Application.LoadLevel("Game");
            }
        }
    }
}