using UnityEngine;

public class PlatformPatrollingEnemyAnimator : HorizontalMovingEnemy {
    private Animator animator;
    public override void ResetState () {
    }
    private void Start () {
        animator = GetComponent<Animator>();
    }
}