using UnityEngine;

public abstract class Damaging : MonoBehaviour
{
  // public Animator animator;
  public Stat attackPower;
  public Stat attackRange;

  public static void DealDamage(GameObject _source, int _damage, GameObject _target)
  {
    if (_target.GetComponent<PlayerHealth>())
    {
      _target.GetComponent<PlayerHealth>().getHit(_damage, _source);
    }
    else if (_target.GetComponent<KnockbackHealth>())
    {
      _target.GetComponent<KnockbackHealth>().getHit(_damage, _source);
    }
    else
    {
      _target.GetComponent<Health>().applyDamage(_damage);
    }
  }
}
