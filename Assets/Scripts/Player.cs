using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 4f;

    Animator anim;
    Vector3 mov;

    void Start()
    {

        anim = GetComponent<Animator>();
        mov = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        );
    }

    void Update()
    {
        mov = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            transform.position + mov,
            speed * Time.deltaTime
        );

        anim.SetFloat("movX", mov.x);
        anim.SetFloat("movY", mov.y);

    }

}