using UnityEngine;

public class BossEnemyAnimator : MonoBehaviour
{
    public readonly bool isFacingRight = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("RockSlide");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
