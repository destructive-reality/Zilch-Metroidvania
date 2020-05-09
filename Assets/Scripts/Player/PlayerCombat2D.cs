using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2D : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform attackPoint;
    public LayerMask enemyLayers;       // remeber assigning layers

    public int playerMaxHealth = 100;
    [SerializeField] private int playerHealth;

    public float attackRange = 0.75f;
    public int playerAttackPower = 20;
    public float attackSpeed = 3f;
    private float nextAttackTime = 0f;

    private float invincibleTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
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
                target.GetComponent<DestroyableObject>().getHit(playerAttackPower);
            else if (targetLayer == 11) // Enemy
                target.GetComponent<EnemyCombat>().getHit(playerAttackPower);
        }
    }

    public void getHit(int damage, GameObject damagingObject)
    {
        if (Time.time >= invincibleTime)
        {
            Debug.Log("Time: " + Time.time + "; invincibleTime: " + invincibleTime);
            playerHealth -= damage;

            // Throw character back from damage source
            var damageSourceDirection = this.transform.position - damagingObject.transform.position;
            transform.Translate((this.transform.position + damageSourceDirection) * Time.deltaTime * 2);

            // Animation goes here
            // animator.SetTrigger("Hurt");
            // if (playerHealth <= 0)
            // {
            //     Die();
            // }
            invincibleTime = Time.time + 5f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
