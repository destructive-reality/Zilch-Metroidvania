using System.Collections.Generic;
using UnityEngine;

public class PointsPatrolBehaviour : PlatformMovementBehaviour
{
    protected new DickEnemyAnimator enemyScript;
    // private float speed;
    private List<Vector2> patrolPoints;
    private int currentTarget = 0;
    
    // similar to walkTowardsBehaviour; need to create SuperClass /\/\D
    // #region MovementOnPlatform
    // private Transform groundDetector;
    // private float rayDistance = 1f;
    // private LayerMask moveOnLayer;
    // #endregion

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        enemyScript = animator.gameObject.GetComponent<DickEnemyAnimator>();

        patrolPoints = enemyScript.getPatrolPoints();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, animatorStateInfo, layerIndex);
        // RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, rayDistance, moveOnLayer);
        // if (groundInfo.collider == null)
        // {
        //     return;
        // }
        // if (enemyScript.isFacingRight)
        // {
        //     animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
        // }
        // else
        // {
        //     animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
        // }
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