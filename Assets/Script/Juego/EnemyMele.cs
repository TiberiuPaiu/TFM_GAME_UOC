using UnityEngine;

public class EnemyMele : DumbEnemy
{
    public int vida_parametos;
    public SpriteRenderer spriteRenderer;

    [Header("Sprites por nivel")]
    public Sprite[] spritesPorNivel;

    void Start()
    {
        hp = vida_parametos;
        int level = GameManager.Instance.levelActual - 1;


        if (level >= 0 && level < spritesPorNivel.Length)
        {
            spriteRenderer.sprite = spritesPorNivel[level];
        }
    }
}