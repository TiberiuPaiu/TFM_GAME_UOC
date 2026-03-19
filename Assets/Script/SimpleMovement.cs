using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction; // Ahora es privada porque no necesitamos verla en el inspector
    
    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtenemos el Animator al iniciar
    }

    void Update()
    {
        Movement();
        Animate(); // Nueva función para gestionar las animaciones
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x, y).normalized;
    }

    void Animate()
    {
        
        if (direction.magnitude !=  0) 
        {
            // Pasamos los valores de dirección al Blend Tree del Animator
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
            
            // Reproducimos la animación de correr (el Blend Tree)
            animator.Play("Run");
        }
        else 
        {
            // Si está quieto, reproducimos la animación de Idle (parado)
            animator.Play("Idle");
        }
    }
}

    
