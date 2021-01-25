using UnityEngine;
using UnityEngine.Events;

public class BossHealth : KnockbackHealth
{
  [SerializeField] private int[] healthBreakpoints;
  private int nextBreakpoint = 0;
  public UnityEvent reachHealtBreakpoint;

  private void Awake()
  {
    reachHealtBreakpoint = new UnityEvent();
  }

  protected override void Start()
  {
    base.Start();
  }

  public override void applyDamage(int _damageToTake)
  {
    base.applyDamage(_damageToTake);

    if (currentHealth <= healthBreakpoints[nextBreakpoint])
    {
      reachHealtBreakpoint.Invoke();
      nextBreakpoint++;
    }
  }

  protected override void die()
  {
    base.die();
    animator.SetTrigger("die");
  }
}