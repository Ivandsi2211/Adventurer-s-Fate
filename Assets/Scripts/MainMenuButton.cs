using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public Color[] colores;
    public SpriteRenderer buttonSprite;
    public enum buttonStates
    {
        normal = 0,
        selected,
        down
    }
    public buttonStates currentState;
    // Use this for initialization
    void Start()
    {
        currentState = buttonStates.normal;
        buttonSprite = GetComponent<SpriteRenderer>();
        buttonSprite.color = colores[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == buttonStates.normal)
        {
            buttonSprite.color = colores[0];
        } else if (currentState == buttonStates.selected)
        {
            buttonSprite.color = colores[1];
        } else
        {
            buttonSprite.color = colores[2];
        }
    }
}
