using UnityEngine;



public class Trampas : MonoBehaviour
{
    public Sprite trapOn;      // Imagen de pinchos fuera
    public Sprite trapOff;     // Imagen de trampa cerrada
    public float trapTime = 3f; // Tiempo entre cambios (3 segundo)
    public bool trapActive;        // Estado inicial

    private SpriteRenderer sr;
    private BoxCollider2D coll;
     

    private int daño = 10; // Daño que hará la trampa

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        InvokeRepeating("Activate", 0f, trapTime); 
    }

    void Activate()
    {
        // Cambiamos el estado (si estaba activa, pasa a falsa y viceversa)
        trapActive = !trapActive;

        if (trapActive)
        {
            sr.sprite = trapOn;
            coll.enabled = true; 
        }else{
             sr.sprite = trapOff;
             coll.enabled = false; 
        }
   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && trapActive)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
               
                player.TakeDamage(daño, Vector3.zero, "Trap");
            }
        }
    }

    
}