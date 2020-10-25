using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackBehaviour : StateMachineBehaviour
{
    // [SerializeField] private 
    private DoofEnemyAnimator doofEnemyScript;
    private float speed;
    private Vector2 startPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.ResetTrigger("nextAnimation");
        animator.SetBool("isNeedingReset", false);
        Debug.Log("In WalkBackState");
        doofEnemyScript = animator.GetComponent<DoofEnemyAnimator>();
        speed = doofEnemyScript.speed.getValue();
        startPosition = doofEnemyScript.StartPosition;

        Debug.Log(startPosition);
        // animator.SetTrigger("reseted");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CheckStartPositionDirection(animator.transform.position.x))
        {
            if (doofEnemyScript.isFacingRight)
            {
                animator.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            if (!doofEnemyScript.isFacingRight)
            {
                animator.transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
        else
        {
            doofEnemyScript.Flip();
            return;
        }
        if (Vector2.Distance(animator.transform.position, startPosition) <= 1f)
        {
            animator.SetTrigger("reseted");
        }
    }

    private bool CheckStartPositionDirection(float _xPosition)
    {
        // float distance = Vector2.Distance(startPosition, doofEnemyScript.gameObject.transform.position);
        // Debug.Log(distance);
        if (doofEnemyScript.isFacingRight && startPosition.x > _xPosition)
        {
            return true;
        }
        if (!doofEnemyScript.isFacingRight && startPosition.x < _xPosition)
        {
            return true;
        }
        return false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("reseted");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
