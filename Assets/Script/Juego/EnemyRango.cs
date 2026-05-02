using UnityEngine;

public class EnemyRango : DumbEnemy
{
    public int vida_parametos;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por nivel")]
    public Sprite[] spritesPorNivel;

    public float speed = 3f;
    public float distanciaDeteccion = 10f;
    public float distanciaAtaque = 5f;
    public float distanciaMinima = 2.5f; 

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    public AnimatorOverrideController[] controladoresPorNivel;

    private Vector2 direccion;
    private Vector2 ultimaDireccion;

    [Header("Proyectil")]
    public GameObject proyectilPrefab;
    public float tiempoEntreDisparos = 5f;
    private float nextShot;

    private float arrowSpeed = 10f;

    private bool isAttack;

    void Start()
    {
        hp = vida_parametos;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            player = obj.transform;

        // sprite por nivel
        int level = GameManager.Instance.levelActual - 1;
        var data = GameManager.Instance.GetLevel(level);
        int tipo = data.tipoEnemigo;

        spriteRenderer.sprite = spritesPorNivel[tipo % spritesPorNivel.Length];
        animator.runtimeAnimatorController = controladoresPorNivel[tipo % controladoresPorNivel.Length];
    }


    void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.position);

        // Creamos una variable para la dirección visual
        Vector2 direccionMiradaVisual = ultimaDireccion; 

        if (distancia > distanciaDeteccion)
        {
            direccion = Vector2.zero;
        }
        else if (distancia > distanciaAtaque)
        {
            direccion = (player.position - transform.position).normalized;
            direccionMiradaVisual = direccion; // Mirar hacia donde camina
        }
        else if (distancia < distanciaMinima)
        {
            direccion = (transform.position - player.position).normalized;
            direccionMiradaVisual = direccion; // Mirar hacia donde huye
        }
        else
        {
            // QUEDARSE QUIETO Y DISPARAR Quedase quieto mentras ataca
            direccion = Vector2.zero;

            // Aunque no nos movemos, calculamos hacia dónde mirar visualmente
            direccionMiradaVisual = (player.position - transform.position).normalized;

            if (Time.time >= nextShot)
            {
                Disparar(); // Disparar 
                nextShot = Time.time + tiempoEntreDisparos;
            }
        }

        // Actualizamos ultimaDireccion si nos estamos moviendo
        if (direccion.magnitude > 0.1f)
        {
            ultimaDireccion = direccion;
        }

        // Le pasamos la dirección visual calculateda, no la de movimiento
        Animar(direccionMiradaVisual);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * speed;
    }

    void Atacar()
    {
        isAttack = true;
        animator.Play("Attack");
    }

    void ResetAttack()
    {
        isAttack = false;
    }

    void Animar(Vector2 dirVisual)
    {
        // Pasamos al Animator la dirección hacia donde DEBE mirar
        animator.SetFloat("horizontal", dirVisual.x);
        animator.SetFloat("vertical", dirVisual.y);

        // Cambio manual Iden / Run basado en el movimiento REAL
        if (direccion == Vector2.zero)
        {
            animator.Play("Iden");
        }
        else
        {
            animator.Play("Run");
        }
    }

    void Disparar()
    {

        Vector2 dirHaciaJugador = (player.position - transform.position).normalized;

        GameObject arrow = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
        if (arrowRb != null)
        {
            arrowRb.linearVelocity = dirHaciaJugador * arrowSpeed;
        }

        float angle = Mathf.Atan2(dirHaciaJugador.y, dirHaciaJugador.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

     
        if (animator != null)
        {
            animator.Play("Attack");
        }
    }



}