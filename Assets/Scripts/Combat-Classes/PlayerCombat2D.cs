using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2D : Damaging
{
    public PlayerMasterController playerMasterController;
    public Transform attackPoint;
    public LayerMask attackableLayers;       // remeber assigning layers
    public Stat attackTime;
    protected float nextAttackTime = 0.3f;


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
        animator.SetTrigger("Attack");
        dealDamage();
    }
    void dealDamage()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.getValue(), attackableLayers);

        //If anything was hit, do some screen shake
        if (hitTargets.Length > 0)
        {
            CinemachineShake.Instance.ShakeCamera(2.5f, .3f);
        }

        // remember to give enemies 2D-Colliders        MD
        // Debug.Log(hitTargets);
        foreach (Collider2D target in hitTargets)
        {
            // if (!target.isTrigger)
            // {
            int targetLayer = target.gameObject.layer;
            if (targetLayer == LayerMask.NameToLayer("Destroyable") || targetLayer == LayerMask.NameToLayer("Attackable"))
                target.GetComponent<Health>().applyDamage((int)attackPower.getValue());
            else if (targetLayer == LayerMask.NameToLayer("Enemy"))
                target.GetComponent<KnockbackHealth>().getHit((int)attackPower.getValue(), this.gameObject); //Change KnockbackHealth back to EnemyHealth?
            // }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange.getValue());
    }


}
