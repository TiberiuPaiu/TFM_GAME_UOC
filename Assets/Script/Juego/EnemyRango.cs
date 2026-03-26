using UnityEngine;

public class EnemyRango : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por nivel")]
    public Sprite[] spritesPorNivel;

    void Start()
    {
        int level = GameManager.Instance.levelActual - 2;

        if (level >= 0 && level < spritesPorNivel.Length)
        {
            spriteRenderer.sprite = spritesPorNivel[level];
        }
    }
}