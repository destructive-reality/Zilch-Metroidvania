using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBackBehaviour : StateMachineBehaviour
{
    private DoofEnemyAnimator doofEnemyScript;
    private float speed;
    private Vector2 startPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isNeedingReset", false);
        Debug.Log("In WalkBackState");
        doofEnemyScript = animator.GetComponent<DoofEnemyAnimator>();
        speed = doofEnemyScript.speed.getValue();
        startPosition = doofEnemyScript.StartPosition;

        Debug.Log(startPosition);
    }

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
            doofEnemyScript.flip();
            return;
        }
        if (Vector2.Distance(animator.transform.position, startPosition) <= 1f)
        {
            animator.SetTrigger("reseted");
        }
    }

    private bool CheckStartPositionDirection(float _xPosition)
    {
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

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("reseted");
    }
}
