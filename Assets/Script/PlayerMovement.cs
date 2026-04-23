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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Debug.Log("Nivel actual escena 3 - " + GameManager.Instance.levelActual); 
    }

    void Update()
    {
        Movement();
        Animate(); 
    }

    void FixedUpdate()
    {
        // Si está atacando, el personaje no debería deslizarse
        if (isAttack) {
            rb.linearVelocity = Vector2.zero;
        } else {
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
}
