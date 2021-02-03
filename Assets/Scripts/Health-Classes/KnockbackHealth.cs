using UnityEngine;

abstract public class KnockbackHealth : Health
{
  public Stat knockbackForce;
  private Rigidbody2D rb2D;

  protected override void Start()
  {
    base.Start();
    rb2D = GetComponent<Rigidbody2D>();
  }

  public virtual void getHit(int damage, Vector2 damagingObject)
  {
    // Remove ApplyKnockback call here, since only Player uses it   MD
    applyKnockback(new Vector2(this.transform.position.x < damagingObject.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
    applyDamage(damage);
  }

  protected void applyKnockback(Vector2 knockbackForceVector)
  {
    // this.GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
    rb2D.AddForce(knockbackForceVector, ForceMode2D.Impulse);
  }
}