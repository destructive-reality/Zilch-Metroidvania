using UnityEngine;

public class EnemyHealth : KnockbackHealth
{
  protected override void die()
  {
    base.die();
    // Debug.Log("Set trigger death");
    animator.SetTrigger("death");
  }
}