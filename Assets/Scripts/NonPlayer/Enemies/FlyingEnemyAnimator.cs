using UnityEngine;

public class FlyingEnemyAnimator : EnemyBehaviour
{
  // public Stat speed;
  [SerializeField] private float aggressionRange = 10f;
  private Vector2 startPosition;
  private Vector2 playerPosition;
  private Vector2 target;
  private Animator animator;

  public override void ResetState() { }

  public Vector3 GetStartPosition()
  {
    return startPosition;
  }

  private void Start()
  {
    startPosition = new Vector2(transform.position.x, transform.position.y);
    animator = GetComponent<Animator>();
  }

  private void Update()
  {
    playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    target = new Vector2(playerPosition.x, playerPosition.y);

    AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

    if ((currentAnimation.IsName("FlyAround") || currentAnimation.IsName("WaitFor")) &&
    Vector2.Distance(startPosition, target) <= aggressionRange)
    {
      Debug.Log("FlyingEnemy near Player");
      animator.SetBool("isPlayerNear", true);
    }
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    if (startPosition != new Vector2(0, 0))
    {
      Gizmos.DrawWireSphere(startPosition, aggressionRange);
    }
    else
    {
      Gizmos.DrawWireSphere(this.transform.position, aggressionRange);
    }
    Gizmos.color = Color.black;
    Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + (Vector3.down * 2));
  }
}