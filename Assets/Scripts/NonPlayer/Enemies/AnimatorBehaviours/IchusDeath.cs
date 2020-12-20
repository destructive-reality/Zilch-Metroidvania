using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchusDeath : StateMachineBehaviour
{
    public float secondsToDespawn = 8f;

    private float despawnTimer = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Make Ichus fall down
        animator.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        //Disable non-necessary scripts to avoid harmful interaction with the player
        animator.GetComponent<EnemyCombat>().enabled = false;
        animator.GetComponent<Health>().enabled = false;
        animator.GetComponent<FlyingEnemyAnimator>().enabled = false;

        //Change layer to "DeadEnemies"
        animator.gameObject.layer = 15;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= secondsToDespawn)
        {
            Destroy(animator.gameObject);
        }
    }
}
