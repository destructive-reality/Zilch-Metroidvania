using UnityEngine;

public class FlyAroundBehaviour : StateMachineBehaviour
{
  public LayerMask layersToDistanceTo;
  private MovementsBase movementScript;
  private Vector2 startPosition;
  private float speed;
  private float range;
  private Vector2 target;
  private float distanceToStartWait;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator>();
    movementScript = animator.GetComponent<MovementsBase>();
    startPosition = enemyScript.GetStartPosition();
    speed = enemyScript.speed.getValue();
    range = enemyScript.GetRange();
    distanceToStartWait = 1;

    target = startPosition;
    bool noValidTarget = true;
    int counter = 0;
    Vector2 randomV = new Vector2();
    while (noValidTarget && counter < 10)
    {
      float angle = Random.Range(0, 2 * Mathf.PI);
      float targetRange = Random.Range(0, range);
      randomV = new Vector2(range * Mathf.Sin(angle), range * Mathf.Cos(angle));
      RaycastHit2D groundHit = Physics2D.Raycast(animator.transform.position, (target + randomV), range, layersToDistanceTo);
      if (groundHit.collider == null)
      {
        // Debug.Log("Target for " + animator.gameObject.name + " valid");
        noValidTarget = false;
      }
      counter++;
    }
    target = target + randomV;

    if (animator.transform.position.x > target.x)
    {
      movementScript.flip(FLIP_DIRECTION.LEFT);
    }
    else
    {
      movementScript.flip(FLIP_DIRECTION.RIGHT);
    }
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

    float targetDistance = Vector2.Distance(target, animator.transform.position);
    if (targetDistance <= distanceToStartWait)
    {
      animator.SetTrigger("wait");
    }
    else
      distanceToStartWait += 0.02f;
  }
}