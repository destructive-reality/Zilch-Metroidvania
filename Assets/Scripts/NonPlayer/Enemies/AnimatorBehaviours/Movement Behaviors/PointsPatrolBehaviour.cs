using System.Collections.Generic;
using UnityEngine;

public class PointsPatrolBehaviour : PlatformMovementBehaviour
{
    private DickEnemyAnimator dickScript;
    private List<Vector2> patrolPoints;
    private int currentTarget = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);

        enemyScript = animator.gameObject.GetComponent<DickEnemyAnimator>();

        if (enemyScript is DickEnemyAnimator)
        {
            dickScript = enemyScript as DickEnemyAnimator;
            patrolPoints = dickScript.getPatrolPoints();
        }
        else
            Debug.LogWarning("No Dick for PointPatorlBehaviour ");

        if ((animator.transform.position.x < patrolPoints[0].x && animator.transform.position.x < patrolPoints[1].x) ||
            (animator.transform.position.x > patrolPoints[0].x && animator.transform.position.x > patrolPoints[1].x))
        {
            enemyScript.flip();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if ((patrolPoints[currentTarget].x < animator.transform.position.x && enemyScript.isFacingRight) || 
            (patrolPoints[currentTarget].x > animator.transform.position.x && !enemyScript.isFacingRight))
        {
            enemyScript.flip();
        }

        if (Vector2.Distance(animator.transform.position, patrolPoints[currentTarget]) <= 1.5f)
        {
            if (currentTarget + 1 >= patrolPoints.Count)
                currentTarget = 0;
            else
                currentTarget++;
            if ((patrolPoints[currentTarget].x > animator.transform.position.x && !enemyScript.isFacingRight) ||
                (patrolPoints[currentTarget].x < animator.transform.position.x && enemyScript.isFacingRight))
            {
                enemyScript.flip();
            }
        }
    }

    // public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    // {}
}