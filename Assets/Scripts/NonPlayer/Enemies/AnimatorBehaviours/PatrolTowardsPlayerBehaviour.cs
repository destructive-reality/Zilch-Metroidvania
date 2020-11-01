using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTowardsPlayerBehaviour : WalkTowardsBehaviour
{
    private DickEnemyAnimator dickScript;
    private List<Vector2> patrolPoints;
    private float leftX;
    private float rightX;
    private int currentTarget = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        enemyScript = animator.gameObject.GetComponent<DickEnemyAnimator>();
        Debug.Log(enemyScript.GetType());
        if (enemyScript is DickEnemyAnimator)
        {
            dickScript = enemyScript as DickEnemyAnimator;
            patrolPoints = dickScript.getPatrolPoints();
        }
        else
            Debug.LogWarning("No Dick for PatrolTowardsPlayerBehaviour ");
        foreach (Vector2 point in patrolPoints)
        {
            if (point.x < leftX)
            {
                leftX = point.x;
            }
            if (point.x > rightX)
            {
                rightX = point.x;
            }
        }

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (playerPosition.x < leftX - 1 || playerPosition.x > rightX + 1)
        {
            Debug.Log("Dont move over patrolpoint");
            if (leftX - playerPosition.x > 3 || playerPosition.x - rightX > 3)
                animator.SetTrigger("inRangeAttackRange");
            else
                animator.SetTrigger("wait");
            
            return;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("inRangeAttackRange");
        animator.ResetTrigger("seesPlayer");
        // animator.ResetTrigger("wait");
    }
}