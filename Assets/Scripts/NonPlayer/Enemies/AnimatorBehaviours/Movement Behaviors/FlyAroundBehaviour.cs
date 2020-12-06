using UnityEngine;

public class FlyAroundBehaviour : StateMachineBehaviour
{
  public float range;
  public LayerMask layersToDistanceTo;
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
    Debug.Log(target);
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
    RaycastHit2D hitDown = Physics2D.Raycast(animator.transform.position, Vector2.down * 2, raycastDistance, layersToDistanceTo);
    // Check roof distance
    RaycastHit2D hitUp = Physics2D.Raycast(animator.transform.position, Vector2.up * 2, raycastDistance, layersToDistanceTo);
    if (hitDown.collider != null && target.y < animator.transform.position.y && hitDown.distance < 1.5f)
    {
      Debug.Log("dont go lower, " + hitDown.distance);
      // animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
      animator.SetTrigger("wait");
    }
    else if (hitUp.collider != null && target.y > animator.transform.position.y && hitUp.distance < 1.5f)
    {
      Debug.Log("dont go higher, " + hitUp.distance);
      // animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
      animator.SetTrigger("wait");
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