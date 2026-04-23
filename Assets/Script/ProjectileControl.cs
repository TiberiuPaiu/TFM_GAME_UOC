using UnityEngine;
public class ProjectileControl : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float rotationSpeed = 2000f; // Qué tan rápido gira
    public float maxDistance = 7f;    // Distancia máxima antes de destruirse
    
    private Vector3 startPosition;
    [Header("Configuración del daño que el proyectil inflige a los enemigos ")]
    public int damage = 1; // Daño que el proyectil inflige a los enemigos
    private bool hasHit = false;

    void Start()
    {
        // Guardamos la posición desde donde nació el proyectil
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Rotación Continua
        // Giramos el objeto en el eje Z (común en 2D)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // 2. Control de Distancia
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        if (distanceTraveled >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; //  evita doble impacto

        if (collision.CompareTag("Enemy"))
        {
            hasHit = true; //  bloquear inmediato

            DumbEnemy enemy = collision.GetComponent<DumbEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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
        // Aquí podrías instanciar un efecto de partículas antes de destruir
        Destroy(gameObject);
    }
}