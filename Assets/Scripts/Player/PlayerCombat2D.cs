using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2D : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform attackPoint;
    public LayerMask enemyLayers;       // remeber assigning layers

    public float attackRange = 0.75f;
    public int playerAttackPower = 20;
    public float attackSpeed = 3f;
    private float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                // playerState = State.Attacking;
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
    }

    void Attack()
    {
        // playerAnimator.SetTrigger("Attack");

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // remember to give enemies 2D-Colliders        MD
        // Debug.Log(hitTargets);

        foreach (Collider2D target in hitTargets)
        {
            int targetLayer = target.gameObject.layer;
            if (targetLayer == 12)  // Destroyable
                target.GetComponent<Health>().getHit(playerAttackPower);
            else if (targetLayer == 11) // Enemy
                target.GetComponent<EnemyHealth>().getHit(playerAttackPower, this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
