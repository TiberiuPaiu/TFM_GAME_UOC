using UnityEngine;

public partial class DumbEnemy : MonoBehaviour
{
    protected int hp; // Vida del enemigo
    private bool isDead = false;

    private AnimationEnemyDead animationEnemyDead;

    private int dañoContacto = 5;



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


}