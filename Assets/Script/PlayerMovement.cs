using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float arrowSpeed = 10f; 
    private Vector2 direction;
    
    private Rigidbody2D rb;
    private Animator animator;

    private bool isAttack;

    // Cambié arrowPrefab por proyectil para que coincida con tu variable de arriba
    public GameObject proyectil;


    [Header("Invencibilidad")]
    public float invincibilityTime = 1.5f;
    public float blinkTime = 0.1f;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    public float knockbackStrength = 2f;
    public float knockbackTime = 0.2f;

    private bool isKnocked;

    public int curacion = 5;

    public GameObject textocura;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log("Nivel actual escena 3 - " + GameManager.Instance.levelActual); 
    }

    void Update()
    {
        Movement();
        Animate(); 
    }


    void FixedUpdate()
    {
        if (isKnocked)
            return;

        if (isAttack)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = direction * speed;
        }
    }

    void Movement()
    {
        // Si estamos atacando, no procesamos movimiento
        if (isAttack) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x, y).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AvasarPorComadosDeNivel();
        }
    }

    void Animate()
    {
        if (isAttack) return;

        if (direction.magnitude != 0) 
        {
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
            animator.Play("Run");
        }
        else 
        {
            animator.Play("Idle");
        }
    }

    void Attack()
    {
        isAttack = true; 
        
        // Obtener dirección hacia el mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Importante poner Z en 0 en 2D
        Vector2 attackDir = (mousePos - transform.position).normalized;

        // Actualizar el Animator para que dispare hacia donde mira el mouse
        animator.SetFloat("horizontal", attackDir.x);
        animator.SetFloat("vertical", attackDir.y);
        
        animator.Play("Attack");

        //SiguienteNivel();
        
    }

    void ResetAttack()
    {
        isAttack = false;
    }

    void Shoot()
    {
        Vector2 dir = new Vector2(animator.GetFloat("horizontal"), animator.GetFloat("vertical"));
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        GameObject arrow = Instantiate(proyectil, transform.position + (Vector3)dir * 0.5f, Quaternion.Euler(0, 0, angle));
        
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
        arrowRb.linearVelocity = dir * arrowSpeed; 
    }


    public void AvasarPorComadosDeNivel()
    {
        GameManager.Instance.levelActual++;
        GameManager.Instance.cantidadEnemigosEliminados = 0;
        GameManager.Instance.cantidadEnemigos = GameManager.Instance.baseDeDatosNiveles.levels[GameManager.Instance.levelActual - 1].melee + GameManager.Instance.baseDeDatosNiveles.levels[GameManager.Instance.levelActual - 1].rango;

        SceneManager.LoadScene(2); 
    }


    public void TakeDamage(int damage, Vector3 attackerPosition , string controlFuncion)
    {
        if (isInvincible) return;

        GameManager.Instance.vidaJugador -= damage;

        Debug.Log("Vida actual : " + GameManager.Instance.vidaJugador);

        // Hacer el  knockback si NO es trampa
        if (controlFuncion == "Enemy")
        {
            StartCoroutine(KnockbackCoroutine(attackerPosition));
            StartCoroutine(InvisibilityCoroutine());
        }
        else if (controlFuncion == "Trap")
        {
            StartCoroutine(InvisibilityCoroutine());
        }



        if (GameManager.Instance.vidaJugador <= 0)
        {
            Debug.Log("Game over" );
            //SceneManager.LoadScene(4);
        }
    }

    IEnumerator InvisibilityCoroutine()
    {
        if (spriteRenderer == null) yield break;

        isInvincible = true;

        float timer = invincibilityTime;

        while (timer > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkTime);
            timer -= blinkTime;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    IEnumerator KnockbackCoroutine(Vector3 attackerPosition)
    {
        isKnocked = true;

        //Calcula la dirección real: desde el atacante hacia el jugado
        Vector2 pushDirection = (transform.position - attackerPosition).normalized;
        //Debug.Log("pushDirection = " + pushDirection);
        //Debug.Log("transform.position = " + transform.position);
        //Debug.Log("attackerPosition = " + attackerPosition); 
        // Calcular dirección y aplicar fuerza
        rb.linearVelocity = pushDirection * knockbackStrength;


        // Esperar el tiempo de duración del empuje
        yield return new WaitForSeconds(knockbackTime);

        // Detener al enemigo

        isKnocked = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cura"))
        {
            Debug.Log("estoy aqui ");
            if (GameManager.Instance.vidaJugador < 100)
            {
                GameManager.Instance.vidaJugador += curacion;

                if (GameManager.Instance.vidaJugador > 100)
                    GameManager.Instance.vidaJugador = 100;
                MostrarCuracionUI();
                Destroy(collision.gameObject);
            }
        }
    }


    void MostrarCuracionUI()
    {
        StartCoroutine(MostrarCuracionCoroutine());
    }

    IEnumerator MostrarCuracionCoroutine()
    {
        textocura.SetActive(true);

        yield return new WaitForSeconds(1f); // tiempo visible

        textocura.SetActive(false);
    }

}
