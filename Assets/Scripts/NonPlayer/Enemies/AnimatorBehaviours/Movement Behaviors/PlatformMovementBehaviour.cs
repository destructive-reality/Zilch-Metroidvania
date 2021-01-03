using UnityEngine;

public class PlatformMovementBehaviour : StateMachineBehaviour
{
  protected HorizontalMovingEnemy enemyScript;
  protected float speed;

  #region MovementOnPlatform
  protected Transform groundDetector;
  protected float rayDistance = 1f;
  protected LayerMask moveOnLayer;
  #endregion

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    enemyScript = animator.GetComponent<HorizontalMovingEnemy>();
    speed = enemyScript.speed.getValue();
    moveOnLayer = LayerMask.GetMask("Ground");
    groundDetector = enemyScript.groundDetector;
  }

  public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, rayDistance, moveOnLayer);
    if (groundInfo.collider == null || groundInfo.distance < 0.01f)
    {
      return;
    }
    if (enemyScript.isFacingRight)
    {
      animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    else
    {
      animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
  }
}