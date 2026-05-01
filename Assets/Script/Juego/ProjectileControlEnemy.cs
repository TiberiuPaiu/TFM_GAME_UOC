using UnityEngine;
public class ProjectileControlEnemy : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float maxDistance = 7f;    // Distancia máxima antes de destruirse

    private Vector3 startPosition;
    [Header("Configuración del daño que el proyectil inflige a los enemigos ")]
    public int damage = 10; // Daño que el proyectil inflige a los enemigos
    private bool hasHit = false;



    void Start()
    {
        // Guardamos la posición desde donde sale el proyectil
        startPosition = transform.position;
    }

    void Update()
    {
        
  
        //  Control de Distancia
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        if (distanceTraveled >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; //  evita doble impacto

        if (collision.CompareTag("Player"))
        {
            hasHit = true;

            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.TakeDamage(damage, transform.position, "Enemy");
            }

            DestroyProjectile();
        }

        if (collision.CompareTag("World"))
        {
            DestroyProjectile();
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}