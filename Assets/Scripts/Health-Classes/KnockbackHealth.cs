using UnityEngine;

abstract public class KnockbackHealth : Health
{
  public Stat knockbackForce;

  public void getHit(int damage, GameObject damagingObject)
  {
    applyKnockback(new Vector2(this.transform.position.x < damagingObject.transform.position.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
    applyDamage(damage);
  }

  private void applyKnockback(Vector2 knockbackForceVector)
  {
    this.GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
  }
}
