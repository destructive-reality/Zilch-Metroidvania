using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : KnockbackHealth
{
    public float startInvincibleTime = 1.5f;
    [SerializeField] private float invincibleTime = 0f;

    public new void getHit(int damage, GameObject damagingObject)
    {
        if (Time.time >= invincibleTime)
        {
            Debug.Log("Time: " + Time.time + "; invincibleTime: " + invincibleTime);
            currentHealth -= damage;

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

            // Animation goes here
            // animator.SetTrigger("Hurt");
            // if (currentHealth <= 0)
            // {
            //     die();
            // }
            invincibleTime = Time.time + startInvincibleTime;
        }
    }
}
