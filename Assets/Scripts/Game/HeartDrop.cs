using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDrop : MonoBehaviour {
    public float heartLife = 6.0f;

    void Update()
    {
        heartLife -= Time.deltaTime;
        if (heartLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.tag != "Enemy")
        {
            if (col.transform.tag == "Player")
            {
                col.SendMessage("Healed");
            }
            Destroy(gameObject);
        }
    }
}
