using UnityEngine;

public class FlyAroundBehaviour : StateMachineBehaviour
{
  public float range;
  private float speed;
  private Vector2 startPosition;
  private Vector2 target;
  private MovementsBase movementScript;
  [SerializeField] private float raycastDistance = 0.5f;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator>();
    movementScript = animator.GetComponent<MovementsBase>();
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
      movementScript.flipLeft();
    }
    else
    {
      movementScript.flipRight();
    }

    // Check ground distance
    RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, Vector2.down, raycastDistance, LayerMask.NameToLayer("Ground"));
    if (hit.collider != null && target.y < animator.transform.position.y)
    {
      Debug.Log("dont go lower");
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
    }
    else
    {
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);
    }
    // Check roof distance
    hit = Physics2D.Raycast(animator.transform.position, Vector2.up, raycastDistance, LayerMask.NameToLayer("Ground"));
    if (hit.collider != null && target.y > animator.transform.position.y)
    {
      Debug.Log("dont go higher");
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
    }
    else
    {
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);
    }

    float targetDistance = Vector2.Distance(target, animator.transform.position);
    if (targetDistance <= 1f)
    {
      animator.SetTrigger("wait");
    }
  }

  // override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  // }
}