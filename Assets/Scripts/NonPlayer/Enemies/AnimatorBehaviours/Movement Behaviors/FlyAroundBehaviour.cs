using UnityEngine;

public class FlyAroundBehaviour : StateMachineBehaviour {
  public float range;
  public LayerMask layersToDistanceTo;
  private float speed;
  private Vector2 startPosition;
  private Vector2 target;
  private float distanceToStartWait;
  private int[] allowDir = new int[] { 0, 1, 0 };
  private int horizontalTargetMove;
  private int verticalTargetMove;
  private MovementsBase movementScript;

  override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    FlyingEnemyAnimator enemyScript = animator.GetComponent<FlyingEnemyAnimator> ();
    movementScript = animator.GetComponent<MovementsBase> ();
    speed = enemyScript.speed.getValue ();
    startPosition = enemyScript.GetStartPosition ();
    distanceToStartWait = 1;
    horizontalTargetMove = 1;
    verticalTargetMove = 1;

    target = startPosition;
    bool noValidTarget = true;
    int counter = 0;
    Vector2 randomV = new Vector2();
    while (noValidTarget && counter < 10) {
      float angle = Random.Range (0, 2 * Mathf.PI);
      float targetRange = Random.Range (0, range);
      randomV = new Vector2 (range * Mathf.Sin (angle), range * Mathf.Cos (angle));
      RaycastHit2D groundHit = Physics2D.Raycast (animator.transform.position, randomV, range, layersToDistanceTo);
      if (groundHit.collider == null) {
        Debug.Log("Target for " + animator.gameObject.name + " valid");
        noValidTarget = false;
      }
      counter++;
    }
    target = target + randomV;


    // Debug.Log(target);
  }
  override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (animator.transform.position.x > target.x) {
      movementScript.flip (FLIP_DIRECTION.LEFT);
    } else {
      movementScript.flip (FLIP_DIRECTION.RIGHT);
    }

    /// Check ground distance
    // RaycastHit2D hitDown = Physics2D.Raycast(animator.transform.position, Vector2.down * 4, raycastDistance, layersToDistanceTo);
    // // Check roof distance
    // RaycastHit2D hitUp = Physics2D.Raycast(animator.transform.position, Vector2.up * 4, raycastDistance, layersToDistanceTo);
    // if ((hitDown.collider != null && target.y < animator.transform.position.y && hitDown.distance < 1f) ||
    // (hitUp.collider != null && target.y > animator.transform.position.y && hitUp.distance < 1f))
    // {
    //   // Debug.Log("dont go lower, " + hitDown.distance);
    //   animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
    //   distanceToStartWait += 0.5f;
    //   // animator.SetTrigger("wait");
    // }
    // // else if (hitUp.collider != null && target.y > animator.transform.position.y && hitUp.distance < 1f)
    // // {
    // //   Debug.Log("dont go higher, " + hitUp.distance);
    // //   animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(target.x, animator.transform.position.y), speed);
    // //   // animator.SetTrigger("wait");
    // // }
    // else
    // {
    //   animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed);
    // }

    // if (checkDirectionForGround (animator.transform.position, Vector2.down) && target.y < animator.transform.position.y ||
    //   checkDirectionForGround (animator.transform.position, Vector2.up) && target.y > animator.transform.position.y) {
    //   verticalTargetMove = 0;
    //   Debug.Log ("vertical wall");
    //   // animator.transform.position = Vector2.MoveTowards (animator.transform.position, new Vector2 (target.x, animator.transform.position.y), speed);
    //   distanceToStartWait += 0.5f;
    // } else {
    //   verticalTargetMove = 1;
    //   // animator.transform.position = Vector2.MoveTowards (animator.transform.position, target, speed);
    // }
    // if (checkDirectionForGround (animator.transform.position, Vector2.left) && target.y < animator.transform.position.y ||
    //   checkDirectionForGround (animator.transform.position, Vector2.right) && target.y > animator.transform.position.y) {
    //   horizontalTargetMove = 0;
    //   Debug.Log ("horizontal wall");
    //   distanceToStartWait += 0.5f;
    // } else {
    //   horizontalTargetMove = 1;
    // }
    // Vector2 moveTarget = new Vector2 (target.x * allowDir[horizontalTargetMove] + animator.transform.position.x * allowDir[horizontalTargetMove + 1],
    //   target.y * allowDir[verticalTargetMove] + animator.transform.position.x * allowDir[verticalTargetMove + 1]);
    // animator.transform.position = Vector2.MoveTowards (animator.transform.position, moveTarget, speed);
    animator.transform.position = Vector2.MoveTowards (animator.transform.position, target, speed);

    float targetDistance = Vector2.Distance (target, animator.transform.position);
    if (targetDistance <= distanceToStartWait) {
      animator.SetTrigger ("wait");
    }
  }

  private bool checkDirectionForGround (Vector2 _orig, Vector2 _dir, float _dis = 1.5f) {
    RaycastHit2D hit = Physics2D.Raycast (_orig, _dir, _dis, layersToDistanceTo);
    if (hit.collider != null && hit.distance > 0.1f)
      return true;

    return false;
  }
}