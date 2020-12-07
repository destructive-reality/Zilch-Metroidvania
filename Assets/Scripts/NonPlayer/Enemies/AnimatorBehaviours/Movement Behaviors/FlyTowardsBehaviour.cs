using UnityEngine;

public class FlyTowardsBehaviour : StateMachineBehaviour
{
  public float speedMultiplier;
  public LayerMask layersToDistanceTo;
  private float speed;
  private Vector3 target;
  private MovementsBase movementScript;
  [SerializeField] private float raycastDistance = 2f;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    movementScript = animator.GetComponent<MovementsBase>();
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator>();
    speed = enemyScript.speed.getValue() * speedMultiplier;

    target = GameObject.FindGameObjectWithTag("Player").transform.position;
    animator.SetBool("isPlayerNear", false);
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (animator.transform.position.x > target.x)
    {
      movementScript.flip(FLIP_DIRECTION.LEFT);
    }
    else
    {
      movementScript.flip(FLIP_DIRECTION.RIGHT);
    }

    // Check ground distance
    RaycastHit2D hitDown = Physics2D.Raycast(animator.transform.position, Vector2.down, raycastDistance, layersToDistanceTo);
    RaycastHit2D hitUp = Physics2D.Raycast(animator.transform.position, Vector2.up, raycastDistance, layersToDistanceTo);
    if (hitDown.collider != null && target.y < animator.transform.position.y && hitDown.distance < 1)
    {
      Debug.Log("dont go lower");
      // animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, target.y), speed);
      animator.SetTrigger("wait");
    }
    else if (hitUp.collider != null && target.y > animator.transform.position.y && hitUp.distance < 1)
    {
      Debug.Log("dont go higher");
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
}