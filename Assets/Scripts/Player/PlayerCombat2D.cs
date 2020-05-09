using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2D : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int playerMaxHP = 100;
    private int playerHP;

    public float attackRange = 0.75f;
    public int playerAttackPower = 20;
    public float attackSpeed = 3f;
    private float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerMaxHP;
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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // remember to give enemies 2D-Colliders        MD


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyCombat>().getHit(playerAttackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
