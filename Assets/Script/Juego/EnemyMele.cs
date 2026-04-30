using UnityEngine;

public class EnemyMele : DumbEnemy
{
    public int vida_parametos;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por nivel")]
    public Sprite[] spritesPorNivel;

    [Header("IA")]
    public float speed = 3f;
    public float distanciaDeteccion = 8f;
    public float distanciaParada = 1.2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    public AnimatorOverrideController[] controladoresPorNivel;
    private Vector2 direccion;

    private Vector2 ultimaDireccion;

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

        if (level >= 0 && level < spritesPorNivel.Length)
        {
            spriteRenderer.sprite = spritesPorNivel[level];
            animator.runtimeAnimatorController = controladoresPorNivel[level];

        }     
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia < distanciaDeteccion && distancia > distanciaParada)
        {
            direccion = (player.position - transform.position).normalized;
        }
      

        Animar();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direccion * speed;
    }

    void Animar()
    {
        if (animator == null) return;

        // Si hay movimiento, actualizamos la "última dirección"
        if (direccion.magnitude > 0.1f)
        {
            ultimaDireccion = direccion;
        }

        // Pasamos al Animator la última dirección conocida, no la actual (que es 0)
        animator.SetFloat("horizontal", ultimaDireccion.x);
        animator.SetFloat("vertical", ultimaDireccion.y);

        // Cambio manual Iden / Run
        if (direccion == Vector2.zero)
        {
            animator.Play("Iden");
        }
        else
        {
            animator.Play("Run");
        }
    }
}