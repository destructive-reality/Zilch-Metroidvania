using UnityEngine;

public class FlyingEnemyAnimator : EnemyBehaviour
{
    public Stat speed;
    [SerializeField] private float aggressionRange = 10f;
    private Vector3 startPosition;
    private Vector3 target;
    private Animator animator;

    public override void ResetState() { }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        target = new Vector3(playerPosition.x, playerPosition.y);

        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

        if ((currentAnimation.IsName("FlyAround") || currentAnimation.IsName("Wait")) &&
        Vector3.Distance(startPosition, target) <= aggressionRange)
        {
            Debug.Log("FlyingEnemy near Player");
            animator.SetBool("isPlayerNear", true);

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (startPosition != new Vector3(0, 0, 0))
        {
            Gizmos.DrawWireSphere(startPosition, aggressionRange);
        }
        else
        {
            Gizmos.DrawWireSphere(this.transform.position, aggressionRange);
        }
    }
}