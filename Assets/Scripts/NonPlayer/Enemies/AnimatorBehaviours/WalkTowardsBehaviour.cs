using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTowardsBehaviour : StateMachineBehaviour
{
    private float speed;
    private Rigidbody2D rigidbody;
    private Vector2 playerPosition;
    private EnemyBehaviour enemyScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPlayerNear", false);

        enemyScript = animator.GetComponent<EnemyBehaviour>();
        speed = enemyScript.speed.getValue();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // rigidbody.velocity = new Vector2(speed, 0);
        if (enemyScript.isFacingRight)
        {
            animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
