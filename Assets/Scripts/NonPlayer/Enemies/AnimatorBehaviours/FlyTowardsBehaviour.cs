using UnityEngine;

public class FlyTowardsBehaviour : StateMachineBehaviour
{
    public float speedMultiplier;
    private float speed;
    private Vector3 target;

    private EnemyMovement enemyMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMovement = animator.GetComponent<EnemyMovement>();
        FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator>();
        speed = enemyScript.speed.getValue() * speedMultiplier;

        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        animator.SetBool("isPlayerNear", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.position.x > target.x)
        {
            enemyMovement.flipLeft();
        }
        else
        {
            enemyMovement.flipRight();
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);

        float targetDistance = Vector2.Distance(target, animator.transform.position);
        if (targetDistance <= 1f)
        {
            animator.SetTrigger("wait");
        }
    }
}