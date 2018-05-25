using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused)
        {
            anim.speed = 0.0f;
        }
        else
        {
            anim.speed = 1.0f;
        }
    }

    public IEnumerator ShowArea(string name)
    {
        anim.Play("Area_Show");
        transform.GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(1).GetComponent<Text>().text = name;
        yield return new WaitForSeconds(0.6f);
        anim.Play("Area_FadeOut");
    }
}