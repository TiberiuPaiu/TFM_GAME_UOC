using UnityEngine;
using System.Collections;

public class BossFinal : DumbEnemy
{
    [Header("Vida")]
    public int vida_parametos = 10;
    private int hpMax;

    [Header("Referencias")]
    private Transform player;
    private Animator animator;

    [Header("Proyectil")]
    public GameObject proyectilRafaga;
    public GameObject proyectilAbanico;
    public GameObject proyectilCircular;
    public float arrowSpeed = 8f;

    [Header("Ráfaga")]
    public int balasPorRafaga = 5;
    public float tiempoEntreBalas = 0.2f;
    public float tiempoEntreRafagas = 0.3f;

    private float nextAttack;

    private Vector2 ultimaDireccion;

    private bool isAttacking = false;

    void Start()
    {
        hp = vida_parametos;
        hpMax = hp;
        animator = GetComponent<Animator>();

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            player = obj.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        ultimaDireccion = dir;

        Animar(dir);

        if (Time.time >= nextAttack)
        {
            EjecutarAtaque();
            nextAttack = Time.time + tiempoEntreRafagas;
        }
    }

  
    void EjecutarAtaque()
    {
        float vidaPorcentaje = (float)hp / hpMax;

        // FASE 1 (> 75%)
        if (vidaPorcentaje > 0.75f)
        {
            StartCoroutine(Rafaga());
        }
        // FASE 2 (<= 50%)
        else if (vidaPorcentaje > 0.50f)
        {
            int tipo = Random.Range(0, 2);

            switch (tipo)
            {
                case 0:
                    StartCoroutine(Rafaga());
                    break;
                case 1:
                    DisparoEnAbanico(6, 90f);
                    break;
            }
        }
        // FASE 3 (<= 30%)
        else
        {
            int tipo = Random.Range(0, 3);

            switch (tipo)
            {
                case 0:
                    StartCoroutine(Rafaga());
                    break;
                case 1:
                    DisparoEnAbanico(6, 90f);
                    break;
                case 2:
                    DisparoCircular(12);
                    break;
            }
        }

        
    }

    IEnumerator Rafaga()
    {
        isAttacking = true;
        animator.Play("Atack");

        for (int i = 0; i < balasPorRafaga; i++)
        {
            DispararRafaga();
            yield return new WaitForSeconds(tiempoEntreBalas);
        }
        isAttacking = false;
    }

    void DispararRafaga()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(proyectilRafaga, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * arrowSpeed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void DisparoEnAbanico(int cantidad, float anguloTotal)
    {
        isAttacking = true;
        animator.Play("Attack"); 

        Vector2 dirBase = (player.position - transform.position).normalized;
        float anguloBase = Mathf.Atan2(dirBase.y, dirBase.x) * Mathf.Rad2Deg;

        float anguloInicio = anguloBase - (anguloTotal / 2);

        for (int i = 0; i < cantidad; i++)
        {
            float angulo = anguloInicio + (anguloTotal / (cantidad - 1)) * i;
            float rad = angulo * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = Instantiate(proyectilAbanico, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * arrowSpeed;

            bullet.transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
        StartCoroutine(ResetAttack());
    }

    void DisparoCircular(int cantidad)
    {
        isAttacking = true;
        animator.Play("Atack");
        for (int i = 0; i < cantidad; i++)
        {
            float angulo = (360f / cantidad) * i;
            float rad = angulo * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = Instantiate(proyectilCircular, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = dir * arrowSpeed;

            bullet.transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
        StartCoroutine(ResetAttack());
    }


    void Animar(Vector2 dir)
    {
        if (animator == null) return;

        if (!isAttacking)
        {
            animator.Play("Iden");
        }
    }


    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.31f); // ajusta según tu animación
        isAttacking = false;
    }

}