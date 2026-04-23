using UnityEngine;

public partial class DumbEnemy : MonoBehaviour
{
    protected int hp; // Vida del enemigo
    private bool isDead = false;


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;

        Debug.Log("Enemigo golpeado. Vida: " + hp);

        if (hp <= 0)
        {
            isDead = true;

            GameManager.Instance.EnemyKilled();
            GameManager.Instance.ComprobarFinNivel();

            Destroy(gameObject);
        }
    }


}