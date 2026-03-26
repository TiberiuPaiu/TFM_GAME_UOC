using UnityEngine;

public class EnemyMele : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por nivel")]
    public Sprite[] spritesPorNivel;

    void Start()
    {
        int level = GameManager.Instance.levelActual - 1;

        if (level >= 0 && level < spritesPorNivel.Length)
        {
            spriteRenderer.sprite = spritesPorNivel[level];
        }
    }
}