using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofEnemyAnimator : EnemyBehaviour
{
    // public Stat speed;
    public float attackRange;
    [SerializeField] private float aggressionRange = 7f;
    private Animator animator;
    private Vector2 position;
    private Vector2 startPosition;
    private Vector2 playerPosition;
    // private Vector2 target;

    void Start()
    {
        position = gameObject.transform.position;
        startPosition = new Vector2(position.x, position.y);
        animator = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        position = gameObject.transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

        if (!currentAnimation.IsName("WalkTowards") && Vector2.Distance(position, playerPosition) <= aggressionRange && !animator.GetBool("isPlayerNear"))
        {
            Debug.Log("DoofEnemy is near Player");
            animator.SetBool("isPlayerNear", true);
        }
        else if (currentAnimation.IsName("WalkTowards"))
        {
            if (playerPosition.x < position.x && isFacingRight)
            {
                Flip();
            }
            else if (playerPosition.x > position.x && !isFacingRight)
            {
                Flip();
            }
            if (Vector2.Distance(position, playerPosition) > aggressionRange && !animator.GetBool("isNeedingReset"))
            {
                Debug.Log("Player out of Doof's aggro range");
                animator.SetBool("isNeedingReset", true);
            }
            else if (Vector2.Distance(position, playerPosition) < attackRange)
            {
                Debug.Log("Doof in attackRange");
                animator.SetTrigger("attackRange");
            }
        }
        else if (currentAnimation.IsName("Attack") && Vector2.Distance(position, playerPosition) > aggressionRange)
        {
            Debug.Log("Player out of Doof's aggro range");
            animator.SetBool("isNeedingReset", true);
        }
        else if (currentAnimation.IsName("Reset") && Vector2.Distance(startPosition, position) < 2f)
        {
            Debug.Log("Doof back to Idle");
            animator.SetTrigger("reseted");
        }
    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, aggressionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
}
