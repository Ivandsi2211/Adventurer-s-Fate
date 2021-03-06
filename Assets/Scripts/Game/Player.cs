﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed = 4f;
    public GameObject initialMap;
    public GameObject slashPrefab;

    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;  // Ahora es visible entre los métodos

    CircleCollider2D attackCollider;

    Aura aura;

    AnimatorStateInfo stateInfo;
    bool movePrevent;
    bool attacking;
    bool loading;

    float chargedAttackTime;

    public AudioClip swordSlashClip;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    public int maxHearts = 4;
    public int startHearts = 3;
    public int healthPerHeart = 4;
    ///--- Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int currentMaxHp;
    [Tooltip("Vida actual")]
    public int hp;

    public Image[] healthImages;
    public Sprite[] healthSprite;

    void Awake()
    {
        Assert.IsNotNull(initialMap);
        Assert.IsNotNull(slashPrefab);

        source = GetComponent<AudioSource>();
        currentMaxHp = startHearts * healthPerHeart;
        hp = currentMaxHp;
        checkHeartAmount();
        UpdateHearts();
    }

    void checkHeartAmount()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
    }

    public void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprite[0];
            }
            else
            {
                i++;
                if (hp >= i * healthPerHeart)
                {
                    image.sprite = healthSprite[healthSprite.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - hp));
                    int healthPerImage = healthPerHeart / (healthSprite.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprite[imageIndex];
                    empty = true;
                }
            }
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //* Recuperamos el collider de ataque y lo desactivamos
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;

        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

        aura = transform.GetChild(1).GetComponent<Aura>();

        attacking = false;
        loading = false;

        chargedAttackTime = 0.0f;
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

        // Detectamos el movimiento
        Movements();

        // Procesamos las animaciones
        Animations();


        // Ataque con espada
        SwordAttack();

        // Ataque con rayo maestro
        SlashAttack();

        // Prevenir movimiento
        PreventMovement();

    }

    void FixedUpdate()
    {
        // Nos movemos en el fixed por las físicas
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }

    void Movements()
    {
        // Detectamos el movimiento en un vector 2D
        if (!PauseMenu.gameIsPaused)
        {
            mov = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
        }
        else
        {
            mov = Vector2.zero;
        }
    }

    void Animations()
    {
        if (mov != Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    void SwordAttack()
    {
        // Vamos actualizando la posición de la colisión de ataque
        if (mov != Vector2.zero)
        {
            attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);
        }

        // Buscamos el estado actual mirando la información del animador
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("Player_Attack");
        loading = stateInfo.IsName("Player_Slash");

        // Detectamos el ataque, tiene prioridad por lo que va abajo del todo
        if (!Input.GetButton("Attack") && loading && !attacking && !PauseMenu.gameIsPaused)
        {
            chargedAttackTime = 0.0f;
            anim.SetTrigger("attacking");
            float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(swordSlashClip, vol);
        }

        // Activamos el collider a la mitad de la animación de ataque
        if (attacking && !PauseMenu.gameIsPaused)
        { // El normalized siempre resulta ser un ciclo entre 0 y 1 
            float playbackTime = stateInfo.normalizedTime;

            if (playbackTime > 0.33 && playbackTime < 0.66)
            {
                attackCollider.enabled = true;
            }
            else
            {
                attackCollider.enabled = false;
            }
        }

    }

    void SlashAttack()
    {
        // Buscamos el estado actual mirando la información del animador
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("Player_Attack");
        loading = stateInfo.IsName("Player_Slash");

        // Ataque a distancia
        if (Input.GetButton("Attack") && !attacking && !PauseMenu.gameIsPaused)
        {
            movePrevent = true;
            chargedAttackTime += Time.deltaTime;
            anim.SetTrigger("loading");
            if (chargedAttackTime >= aura.getTimeBeforePlay())
            {
                aura.AuraStart();
            }
        }
        else if (aura.IsLoaded() && !attacking && !PauseMenu.gameIsPaused)
        {
            chargedAttackTime = 0.0f;
            anim.SetTrigger("attacking");
            float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(swordSlashClip, vol);
            // Para que se mueva desde el principio tenemos que asignar un
            // valor inicial al movX o movY en el edtitor distinto a cero
            float angle = Mathf.Atan2(
                anim.GetFloat("movY"),
                anim.GetFloat("movX")
            ) * Mathf.Rad2Deg;

            GameObject slashObj = Instantiate(
                slashPrefab, transform.position,
                Quaternion.AngleAxis(angle, Vector3.forward)
            );

            Slash slash = slashObj.GetComponent<Slash>();
            slash.mov.x = anim.GetFloat("movX");
            slash.mov.y = anim.GetFloat("movY");

            aura.AuraStop();
            StartCoroutine(EnableMovementAfter(0.4f));
        }
        else
        {
            StartCoroutine(EnableMovementAfter(0.4f));
        }
    }

    void PreventMovement()
    {
        if (movePrevent)
        {
            mov = Vector2.zero;
        }
    }

    IEnumerator EnableMovementAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movePrevent = false;
    }

    public void Attacked()
    {
        hp--;
        UpdateHearts();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Healed()
    {
        if(hp + 4 >= currentMaxHp)
        {
            hp = currentMaxHp;
        }
        else
        {
            hp += 4;
        }
        UpdateHearts();
    }
}