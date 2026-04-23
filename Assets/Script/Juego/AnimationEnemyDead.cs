using UnityEngine;

public sealed class AnimationEnemyDead : MonoBehaviour
{
    private Animator anim;

    public GameObject spriteDeAnimacion;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayDeathEffect()
    {
        
        anim.Play("EnemyDead");
    }

    public void FinalDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}