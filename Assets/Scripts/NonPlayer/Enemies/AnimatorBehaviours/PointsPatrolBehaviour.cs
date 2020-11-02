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

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);

        if (Vector2.Distance(animator.transform.position, patrolPoints[currentTarget]) <= 1)
        {
            if (currentTarget + 1 >= patrolPoints.Count)
                currentTarget = 0;
            else
                currentTarget++;
            enemyScript.Flip();
        }
    }

    // public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    // {}
}