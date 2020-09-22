﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatrolBehaviour : StateMachineBehaviour {
    private float speed;
    private Transform groundDetection;
    private float rayDistance = 1f;
    private LayerMask moveOnLayer;
    private PlatformPatrollingEnemyAnimator enemyScript;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyScript = animator.GetComponent<PlatformPatrollingEnemyAnimator> ();
        speed = enemyScript.speed.getValue ();
        groundDetection = enemyScript.groundDetector;

        moveOnLayer = LayerMask.GetMask ("Ground");

    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, rayDistance, moveOnLayer);
        if (groundInfo.collider == null) {
            Debug.Log ("PatrollingEnemy needs to flip");
            enemyScript.Flip ();
        }
        if (enemyScript.isFacingRight) {
            animator.transform.Translate (Vector2.right * speed * Time.deltaTime);
        } else {
            animator.transform.Translate (Vector2.left * speed * Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //}
}