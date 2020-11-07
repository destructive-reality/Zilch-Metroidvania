using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2D : CombatBase
{
    public PlayerMasterController playerMasterController;
    public Transform attackPoint;
    public LayerMask attackableLayers;       // remeber assigning layers

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                startAttack();
                // playerState = State.Attacking;
                nextAttackTime = Time.time + attackTime.getValue();
            }
        }
    }

    void startAttack()
    {
        // animator.SetTrigger("Attack");
        dealDamage();
    }
    void dealDamage()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.getValue(), attackableLayers);
        // remember to give enemies 2D-Colliders        MD
        // Debug.Log(hitTargets);
        foreach (Collider2D target in hitTargets)
        {
            if (!target.isTrigger)
            {
                int targetLayer = target.gameObject.layer;
                if (targetLayer == LayerMask.NameToLayer("Destroyable") || targetLayer == LayerMask.NameToLayer("Attackable"))
                    target.GetComponent<Health>().applyDamage((int) attackPower.getValue());
                else if (targetLayer == LayerMask.NameToLayer("Enemy"))
                    target.GetComponent<KnockbackHealth>().getHit((int) attackPower.getValue(), this.gameObject); //Change KnockbackHealth back to EnemyHealth?
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange.getValue());
    }


}
