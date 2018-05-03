using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGameButtonController : MonoBehaviour
{
    public GameObject upSprite;
    public GameObject selectedSprite;
    public GameObject downSprite;
    public float downTime = 0.1f;
    private MainMenuButton.buttonStates currentState = MainMenuButton.buttonStates.normal;
    private float nextStateTime = 0.0f;
    void Start()
    {
        upSprite.SetActive(true);        selectedSprite.SetActive(true);        downSprite.SetActive(true);
    }
    void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == MainMenuButton.buttonStates.normal)
        {
            nextStateTime = Time.time + downTime;
            upSprite.SetActive(true);
            downSprite.SetActive(true);
            currentState = MainMenuButton.buttonStates.down;
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
                selectedSprite.SetActive(true);
                downSprite.SetActive(true);
                currentState = MainMenuButton.buttonStates.normal;
                // Començar el joc
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            }
        }
    }


}