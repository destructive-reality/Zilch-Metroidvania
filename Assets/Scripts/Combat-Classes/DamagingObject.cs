using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : Damaging
{
    private CircleCollider2D damageCollider;

    // Start is called before the first frame update
    void Start()
    {
        // attackPower = 10;
        // attackRange = 0.87f;
        damageCollider = GetComponent<CircleCollider2D>();
        damageCollider.radius = attackRange.getValue();
        // Debug.Log(attackRange);
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        Damaging.DealDamage(gameObject, (int) attackPower.getValue(), collider.gameObject);
        // collider.GetComponent<PlayerHealth>().getHit(attackPower, gameObject);
    }

    // draw the Radius on the object
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange.getValue());
    }

}
