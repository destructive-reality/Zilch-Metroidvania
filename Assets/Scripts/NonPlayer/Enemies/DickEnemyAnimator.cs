using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DickEnemyAnimator : HorizontalMovingEnemy
{
    public Transform[] patrolPointTransforms;
    [SerializeField] private float aggressionRange = 5;
    private Animator animator;
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

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        patrolPoints = new List<Vector2>();
        foreach (Transform trn in patrolPointTransforms)
        {
            patrolPoints.Add(new Vector2(trn.position.x, trn.position.y));
        }
        // foreach (Transform transform in patrolPointTransforms)  // Might as well delete uneccessary GOs /\/\D
        // {
        //     GameObject.Destroy(transform.gameObject);
        // }

    }

    private void Update()
    {
        // Draw raycast to look for Player in facing-direction when not attacking
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (!animatorState.IsName("PatrolTowardsPlayer"))
        {
            if (isFacingRight)
                rayDirection = Vector2.right;
            else
                rayDirection = Vector2.left;
            rayDirection.x *= aggressionRange;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, isFacingRight ? Vector2.right : Vector2.left, aggressionRange, LayerMask.GetMask("Player"));
            // Debug.Log(raycastHit2D.collider.tag);
            if (raycastHit2D.collider)
            {
                animator.SetTrigger("seesPlayer");
                // Debug.Log("seeing Player");
            }
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.red;
        // Vector2 gizmoTarget = new Vector2(this.transform.position.x, this.transform.position.y);
        // if (isFacingRight)
        //     gizmoTarget.x += aggressionRange;
        // else
        //     gizmoTarget.x -= aggressionRange;
        // Gizmos.DrawLine(this.transform.position, gizmoTarget);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, new Vector2(rayDirection.x, 0));
    }
}