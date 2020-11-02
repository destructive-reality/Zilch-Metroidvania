using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTowardsPlayerBehaviour : WalkTowardsBehaviour
{
    private DickEnemyAnimator dickScript;
    private List<Vector2> patrolPoints;
    private float leftX;
    private float rightX;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        enemyScript = animator.gameObject.GetComponent<DickEnemyAnimator>();

        dickScript = enemyScript as DickEnemyAnimator;
        patrolPoints = dickScript.getPatrolPoints();

        leftX = rightX = patrolPoints[0].x;
        foreach (Vector2 point in patrolPoints)
        {
            if (point.x < leftX)
                leftX = point.x;
            if (point.x > rightX)
                rightX = point.x;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((playerPosition.x < leftX && animator.transform.position.x < leftX + 1.5f) || (playerPosition.x > rightX && animator.transform.position.x > rightX - 1.5f))
        {
            Debug.Log("Distance to leftX is " + (leftX - playerPosition.x));
            if ((leftX - playerPosition.x > 3 && leftX - playerPosition.x < 10) || (playerPosition.x - rightX > 3 && playerPosition.x - rightX < 10))
            {
                animator.SetTrigger("inRangeAttackRange");
            }
            else
            {
                animator.SetBool("isNeedingReset", true);
            }
        }
        else
            base.OnStateUpdate(animator, stateInfo, layerIndex);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("inRangeAttackRange");
        animator.ResetTrigger("seesPlayer");
        // animator.ResetTrigger("wait");
    }
}