using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{

    // Tiempo de precarga
    public float waitBeforePlay;

    Animator anim;
    Coroutine manager;
    bool loaded;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AuraStart()
    {
        anim.Play("Aura_Play");
        loaded = true;
    }

    public void AuraStop()
    {
        anim.Play("Aura_Idle");
        loaded = false;
    }

    // Método para comprobar si ya hemos cargado suficiente
    public bool IsLoaded()
    {
        return loaded;
    }

    public float getTimeBeforePlay()
    {
        return waitBeforePlay;
    }
}