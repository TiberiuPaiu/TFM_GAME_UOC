using UnityEngine;

public partial class DumbEnemy : MonoBehaviour
{
    protected int hp; // Vida del enemigo
    private bool isDead = false;

    private AnimationEnemyDead animationEnemyDead;

    



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

}