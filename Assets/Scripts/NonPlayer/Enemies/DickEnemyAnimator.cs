using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DickEnemyAnimator : MeleeHorizontalMovingEnemy
{
    public Transform[] patrolPointTransforms;
    [SerializeField] private float aggressionRange = 5;
    private List<Vector2> patrolPoints;
    private Vector2 rayDirection;

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public List<Vector2> getPatrolPoints()
    {
        return patrolPoints;
    }

    protected override void Start()
    {
        base.Start();
        patrolPoints = new List<Vector2>();
        foreach (Transform trn in patrolPointTransforms)
        {
            patrolPoints.Add(new Vector2(trn.position.x, trn.position.y));
        }
        // foreach (Transform transform in patrolPointTransforms)  // Might as well delete uneccessary GOs MD
        // {
        //     GameObject.Destroy(transform.gameObject);
        // }

    }

    private void Update()
    {
        // Draw raycast to look for Player in facing-direction when not attacking
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("Patrol") || animatorState.IsName("Wait"))
        {
            // if (isFacingRight)
            //     rayDirection = Vector2.right;
            // else
            //     rayDirection = Vector2.left;
            rayDirection = isFacingRight ? Vector2.right : Vector2.left;

            rayDirection.x *= aggressionRange;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, isFacingRight ? Vector2.right : Vector2.left, aggressionRange, LayerMask.GetMask("Player"));
            // Debug.Log(raycastHit2D.collider.tag);
            if (raycastHit2D.collider)
            {
                animator.SetTrigger("seesPlayer");
                // Debug.Log("seeing Player");
            }
        }
        if (animatorState.IsName("PatrolTowardsPlayer"))
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (DistanceToPlayer(combatScript.attackPoint.position) < combatScript.attackRange.getValue())
            {
                animator.SetTrigger("inAttackRange");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, new Vector2(rayDirection.x, 0));
    }
}