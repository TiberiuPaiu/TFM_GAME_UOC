using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del personaje
    public Vector2 direction; // Dirección del movimiento
    
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D

    void Start()
    {
        // Obtenemos el componente RigidBody2D que tiene el jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Llamamos a la función que gestiona los inputs en cada frame
        Movement();
    }

    void FixedUpdate()
    {
        // Aplicamos la velocidad al cuerpo físico en función de la dirección y la velocidad
        rb.linearVelocity = direction * speed;
    }

    void Movement()
    {
        // Capturamos los ejes horizontal y vertical (Teclas WASD / Flechas)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Asignamos los valores al vector de dirección y lo normalizamos
        // La normalización evita que el personaje vaya más rápido al moverse en diagonal
        direction = new Vector2(x, y).normalized;
    }
}


    
