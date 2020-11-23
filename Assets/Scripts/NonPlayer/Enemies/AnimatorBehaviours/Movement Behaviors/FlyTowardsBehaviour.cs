using UnityEngine;

public class FlyTowardsBehaviour : StateMachineBehaviour
{
  public float speedMultiplier;
  private float speed;
  private Vector3 target;
  private MovementsBase movementScript;
  [SerializeField] private float raycastDistance = 0.5f;

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
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, target.y), speed);
    }
    else
    {
      animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);
    }

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
}