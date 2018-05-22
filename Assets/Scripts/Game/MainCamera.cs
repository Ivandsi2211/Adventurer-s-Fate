﻿using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public float smoothTime = 3f;

    Transform target;
    float tLX, tLY, bRX, bRY;

    Vector2 velocity;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        // Forzar la resolución cuadrada en pantalla completa
        Screen.SetResolution(800, 800, FullScreenMode.Windowed);
    }

    void Update()
    {
        // Forzar la resolución si no es cuadrada o pantalla completa
        if (Camera.main.aspect != 1)
        {
            Screen.SetResolution(800, 800, FullScreenMode.Windowed);
        }
        // Permitir cerrar juego al presionar escape
        //if (Input.GetAxis("Cancel/Menu Button") > 0)
        //{
        //    SceneManager.LoadScene("Main", LoadSceneMode.Single);
        //}
        if (target != null)
        {
            float posX = Mathf.Round(
                Mathf.SmoothDamp(transform.position.x,
                    target.position.x, ref velocity.x, smoothTime
                ) * 100) / 100;

            float posY = Mathf.Round(
                Mathf.SmoothDamp(transform.position.y,
                    target.position.y, ref velocity.y, smoothTime
                ) * 100) / 100;

            transform.position = new Vector3(
                Mathf.Clamp(posX, tLX, bRX),
                Mathf.Clamp(posY, bRY, tLY),
                transform.position.z
            );
        }
    }

    public void SetBound(GameObject map)
    {
        Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
        float cameraSize = Camera.main.orthographicSize;

        tLX = map.transform.position.x + cameraSize;
        tLY = map.transform.position.y - cameraSize;
        bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
        bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

        FastMove();
    }

    public void FastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }

}