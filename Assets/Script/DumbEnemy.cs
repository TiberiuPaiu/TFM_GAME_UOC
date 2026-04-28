using UnityEngine;

public partial class DumbEnemy : MonoBehaviour
{
    protected int hp; // Vida del enemigo
    private bool isDead = false;

    private AnimationEnemyDead animationEnemyDead;

    private int dañoContacto = 5;


    public GameObject corazonVida;
    public float probabilidadCorazon = 20f;




    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;

        //Debug.Log("Enemigo golpeado. Vida: " + hp);

        if (hp <= 0)
        {
            destuirEnemigo();
        }
    }


    protected virtual void destuirEnemigo()
    {
        isDead = true;

        GameManager.Instance.EnemyKilled();
        GameManager.Instance.ComprobarFinNivel();

        //Debug.Log("dropsDeEnemigos = " + dropsDeEnemigos);

        generarCorazon();
        //  lanzamos animación
        AnimationEnemyDead anim = GetComponentInChildren<AnimationEnemyDead>();

        if (anim != null)
        {
            anim.PlayDeathEffect();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            if (player != null)
            {

                player.TakeDamage(dañoContacto, transform.position, "Enemy");
            }
        }
    }

    public void generarCorazon()
    {
        float random = Random.Range(0f, 100f);

        if (random <= probabilidadCorazon)
        {
            Instantiate(corazonVida, transform.position , Quaternion.identity);
        }
    }


}