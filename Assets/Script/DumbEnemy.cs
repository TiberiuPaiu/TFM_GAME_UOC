using UnityEngine;

public partial class DumbEnemy : MonoBehaviour
{
    public int hp = 5; // Vida del enemigo


    public void TakeDamage(int damage)
    {
        hp = hp - damage;

        Debug.Log("Enemigo golpeado. Vida: " + hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


}