using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : Damaging
{
    // public int attackPower = 10; Error! See Comment Row 13   MD
    private CircleCollider2D damageCollider;

    // Start is called before the first frame update
    void Start()
    {
        attackPower = 10;       // How to give attackPower in code a value and show it in Unity Editor? Problem due to Inheritance     MD
        attackRange = 0.87f;
        damageCollider = GetComponent<CircleCollider2D>();
        damageCollider.radius = attackRange;
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        collider.GetComponent<PlayerHealth>().getHit(attackPower, gameObject);
    }
    
    // draw the Radius on the object
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, attackRange);
    }

}
