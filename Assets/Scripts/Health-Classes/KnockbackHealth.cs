using UnityEngine;

abstract public class KnockbackHealth : Health
{
  public Stat knockbackForce;

  public virtual void getHit(int damage, GameObject damagingObject)
  {
    // Remove ApplyKnockback call here, since only Player uses it   MD
    // applyKnockback(new Vector2(this.transform.position.x < damagingObject.transform.position.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
    applyDamage(damage);
  }

  protected void applyKnockback(Vector2 knockbackForceVector)
  {
    this.GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
  }
}