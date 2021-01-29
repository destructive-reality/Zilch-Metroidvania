using UnityEngine;

public class DickHealth : EnemyHealth
{
  public override void applyDamage(int _damageToTake)
  {
    base.applyDamage(_damageToTake);
    animator.SetTrigger("gotAttacked");
    Debug.Log("Got Attacked");
  }
}