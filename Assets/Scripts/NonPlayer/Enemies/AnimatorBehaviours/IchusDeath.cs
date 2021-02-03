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
    Rigidbody2D rb2D = animator.GetComponent<Rigidbody2D>();
    rb2D.bodyType = RigidbodyType2D.Dynamic;
    rb2D.constraints = RigidbodyConstraints2D.None;
    rb2D.gravityScale = 1;

    //Disable non-necessary scripts to avoid harmful interaction with the player
    animator.GetComponent<EnemyCombat>().enabled = false;
    animator.GetComponent<Health>().enabled = false;
    animator.GetComponent<FlyingEnemyAnimator>().enabled = false;

    //Change layer to "DeadEnemies"
    animator.gameObject.layer = 15;

    Destroy(animator.gameObject, secondsToDespawn);
  }
}
