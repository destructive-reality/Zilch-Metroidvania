using UnityEngine;

public class WalkTowardsBehaviour : PlatformMovementBehaviour
{
  protected Transform playerTransform;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    // playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    if ((animator.transform.position.x > playerTransform.position.x && enemyScript.isFacingRight) || (animator.transform.position.x < playerTransform.position.x && !enemyScript.isFacingRight))
    {
      enemyScript.flip();
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.SetBool("isPlayerNear", false);
    animator.SetBool("isNeedingReset", false);
  }
}