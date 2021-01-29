public class AttackableHealth : Health
{
  public override void applyDamage(int damage)
  {
    onHit();
    base.applyDamage(damage);
  }
  protected virtual void onHit()
  {
    // nothing at first
  }
}
