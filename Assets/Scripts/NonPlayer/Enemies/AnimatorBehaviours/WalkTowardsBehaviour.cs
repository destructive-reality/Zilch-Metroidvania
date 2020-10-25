using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTowardsBehaviour : StateMachineBehaviour
{
    private float speed;
    private Rigidbody2D rigidbody;
    private Vector2 playerPosition;
    private DoofEnemyAnimator enemyScript;

    #region MovementOnPlatform
    private Transform groundDetector;
    private float rayDistance = 1f;
    private LayerMask moveOnLayer;
    #endregion

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyScript = animator.GetComponent<DoofEnemyAnimator>();
        speed = enemyScript.speed.getValue();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        groundDetector = enemyScript.groundDetector;
        moveOnLayer = LayerMask.GetMask ("Ground");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast (groundDetector.position, Vector2.down, rayDistance, moveOnLayer);
        if (groundInfo.collider == null)
        {
            return;
        }
        if (enemyScript.isFacingRight)
        {
            animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlayerNear", false);
        animator.SetBool("isNeedingReset", false);
    }
}