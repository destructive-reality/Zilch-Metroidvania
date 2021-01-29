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
    if ((playerTransform.position.x < leftX && animator.transform.position.x < leftX + 1.5f) || (playerTransform.position.x > rightX && animator.transform.position.x > rightX - 1.5f))
    {
      Debug.Log("Distance to Player is " + Vector2.Distance(playerTransform.position, animator.transform.position));
      if (playerInAttackRange(0, 4, animator))
      {
        animator.SetTrigger("inAttackRange");
      }
      else if (playerInAttackRange(4, 12, animator))
      {
        animator.SetTrigger("inRangeAttackRange");
      }
      else
      {
        animator.SetBool("isNeedingReset", true);
        return;
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

  private bool playerInAttackRange(float _minRange, float _maxRange, Animator animator)
  {
    // if (Mathf.Abs(playerTransform.position.x - animator.transform.position.x) > _minRange && Mathf.Abs(playerTransform.position.x - animator.transform.position.x) < _maxRange)
    if (Vector2.Distance(playerTransform.position, animator.transform.position) >= _minRange && Vector2.Distance(playerTransform.position, animator.transform.position) < _maxRange)
    {
      return true;
    }
    return false;
  }
}