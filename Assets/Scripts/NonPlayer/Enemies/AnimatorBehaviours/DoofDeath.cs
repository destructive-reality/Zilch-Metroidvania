using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofDeath : StateMachineBehaviour
{
    public float secondsToDespawn = 8f;

    private float despawnTimer = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Disable non-necessary scripts to avoid harmful interaction with the player
        animator.GetComponent<EnemyBehaviour>().enabled = false;
        animator.GetComponent<Health>().enabled = false;

        //Change layer to "DeadEnemies"
        animator.gameObject.layer = 15;

        Destroy(animator.gameObject, secondsToDespawn);
    }
}
