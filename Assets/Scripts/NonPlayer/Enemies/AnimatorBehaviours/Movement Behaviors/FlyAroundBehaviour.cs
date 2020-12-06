using UnityEngine;

public class FlyAroundBehaviour : StateMachineBehaviour
{
  public float range;
  private float speed;
  private Vector2 startPosition;
  private Vector2 target;

  private EnemyMovement enemyMovement;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator>();
    enemyMovement = animator.GetComponent<EnemyMovement>();
    speed = enemyScript.speed.getValue();
    startPosition = enemyScript.GetStartPosition();

    target = startPosition;

    float angle = Random.Range(0, 2 * Mathf.PI);
    float targetRange = Random.Range(0, range);
    Vector2 randomV = new Vector2(range * Mathf.Sin(angle), range * Mathf.Cos(angle));

    target = target + randomV;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (animator.transform.position.x > target.x)
    {
      enemyMovement.flip(FLIP_DIRECTION.LEFT);
    }
    else
    {
      enemyMovement.flip(FLIP_DIRECTION.RIGHT);
    }

    animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);

    float targetDistance = Vector2.Distance(target, animator.transform.position);
    if (targetDistance <= 1f)
    {
      animator.SetTrigger("wait");
    }
  }

  // override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  // }
}