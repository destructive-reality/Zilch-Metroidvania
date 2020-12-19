using UnityEngine;

public class FlyTowardsBehaviour : StateMachineBehaviour {
  public float speedMultiplier;
  public LayerMask layersToDistanceTo;
  private float speed;
  private Vector3 target;
  private float distanceToStartWait;
  private int[] allowDir = new int[] { 0, 1, 0 };
  private int horizontalTargetMove;
  private int verticalTargetMove;
  private MovementsBase movementScript;

  override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    movementScript = animator.GetComponent<MovementsBase> ();
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator> ();
    speed = enemyScript.speed.getValue () * speedMultiplier;

    target = GameObject.FindGameObjectWithTag ("Player").transform.position;
    distanceToStartWait = 1;
    horizontalTargetMove = 1;
    verticalTargetMove = 1;
    animator.SetBool ("isPlayerNear", false);
  }

  override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (animator.transform.position.x > target.x) {
      movementScript.flip (FLIP_DIRECTION.LEFT);
    } else {
      movementScript.flip (FLIP_DIRECTION.RIGHT);
    }

    // Check ground distance
    // RaycastHit2D hitDown = Physics2D.Raycast(animator.transform.position, Vector2.down, raycastDistance, layersToDistanceTo);
    // RaycastHit2D hitUp = Physics2D.Raycast(animator.transform.position, Vector2.up, raycastDistance, layersToDistanceTo);
    if (checkDirectionForGround (animator.transform.position, Vector2.down) && target.y < animator.transform.position.y ||
      checkDirectionForGround (animator.transform.position, Vector2.up) && target.y > animator.transform.position.y) {
      verticalTargetMove = 0;
      // animator.transform.position = Vector2.MoveTowards (animator.transform.position, new Vector2 (target.x, animator.transform.position.y), speed);
      distanceToStartWait += 0.5f;
    } else {
      verticalTargetMove = 1;
      // animator.transform.position = Vector2.MoveTowards (animator.transform.position, target, speed);
    }
    if (checkDirectionForGround (animator.transform.position, Vector2.left) && target.y < animator.transform.position.y ||
      checkDirectionForGround (animator.transform.position, Vector2.right) && target.y > animator.transform.position.y) {
      horizontalTargetMove = 0;
      distanceToStartWait += 0.5f;
    } else {
      horizontalTargetMove = 1;
    }
    Vector2 moveTarget = new Vector2 (target.x * allowDir[horizontalTargetMove] + animator.transform.position.x * allowDir[horizontalTargetMove + 1],
      target.y * allowDir[verticalTargetMove] + animator.transform.position.x * allowDir[verticalTargetMove + 1]);
    animator.transform.position = Vector2.MoveTowards (animator.transform.position, moveTarget, speed);

    float targetDistance = Vector2.Distance (target, animator.transform.position);
    if (targetDistance <= distanceToStartWait) {
      animator.SetTrigger ("wait");
    }
  }

  private bool checkDirectionForGround (Vector2 _orig, Vector2 _dir, float _dis = 1) {
    RaycastHit2D hit = Physics2D.Raycast (_orig, _dir, _dis, layersToDistanceTo);
    if (hit.collider != null)
      return true;

    return false;
  }
}