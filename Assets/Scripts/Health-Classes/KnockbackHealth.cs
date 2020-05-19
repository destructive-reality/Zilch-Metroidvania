using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class KnockbackHealth : Health
{
    [SerializeField] protected float knockbackForce = 50f;

    public void getHit(int damage, GameObject damagingObject)
    {
        currentHealth -= damage;

        if (knockbackForce > 0)
        {
            // Throw character back from damage source
            var damageSourceDirection = this.transform.position - damagingObject.transform.position;
            if (damageSourceDirection.x >= 0)
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackForce, knockbackForce / 2), ForceMode2D.Impulse);
            }
            else
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0 - knockbackForce, knockbackForce / 4), ForceMode2D.Impulse);
            }
        }

        // Animation goes here
        // animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            die();
        }
    }
}
