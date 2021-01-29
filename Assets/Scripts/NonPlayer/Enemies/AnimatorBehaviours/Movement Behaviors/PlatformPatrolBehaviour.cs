using UnityEngine;

public class PlatformPatrolBehaviour : StateMachineBehaviour
{
  private float speed;
  private Transform groundDetection;
  private float rayDistance = 1f;
  private LayerMask moveOnLayer;
  private PlatformPatrollingEnemyAnimator enemyScript;

  private Transform parentTransform;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    parentTransform = animator.transform;
    enemyScript = parentTransform.GetComponent<PlatformPatrollingEnemyAnimator>();
    speed = enemyScript.speed.getValue();
    groundDetection = enemyScript.groundDetector;

    moveOnLayer = LayerMask.GetMask("Ground");

  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, moveOnLayer);
    if (groundInfo.collider == null || groundInfo.distance < 0.1f)
    {
      // Debug.Log ("PatrollingEnemy needs to flip");
      enemyScript.flip();
    }
    if (enemyScript.isFacingRight)
    {
      parentTransform.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    else
    {
      parentTransform.transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  //}
}