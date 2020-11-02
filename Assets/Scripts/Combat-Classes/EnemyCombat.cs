using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatBase
{
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerHealth>().getHit((int) attackPower.getValue(), gameObject);
        }
    }
}