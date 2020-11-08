using UnityEngine;

public class MeleeEnemyCombat : EnemyCombat
{
    public Transform attackPoint;
    public Collider2D damagingCollider;

    private void Start()
    {
        // nextAttackTime = 1.5f;
    }

    public void Attack()
    {
        damagingCollider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (damagingCollider.enabled)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Damaging Player");
                collider.GetComponent<PlayerHealth>().getHit((int)attackPower.getValue(), gameObject);
            }
            damagingCollider.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange.getValue());
    }
}