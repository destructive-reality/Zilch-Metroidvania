using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class KnockbackHealth : Health
{
    [SerializeField] protected float knockbackForce = 50f;

    public void getHit(int damage, GameObject damagingObject)
    {
        applyKnockback(new Vector2(this.transform.position.x < damagingObject.transform.position.x ? knockbackForce : -knockbackForce, knockbackForce / 4));
        base.applyDamage(damage);
    }

    private void applyKnockback(Vector2 knockbackForceVector)
    {
        this.GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
    }
}
