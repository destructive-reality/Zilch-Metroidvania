using UnityEngine;

public class WalkTowardsBehaviour : PlatformMovementBehaviour
{
    protected Vector2 playerPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if ((animator.transform.position.x > playerPosition.x && enemyScript.isFacingRight) || (animator.transform.position.x < playerPosition.x && !enemyScript.isFacingRight))
        {
            enemyScript.Flip();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlayerNear", false);
        animator.SetBool("isNeedingReset", false);
    }
}