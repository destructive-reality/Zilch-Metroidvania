using UnityEngine;

public class PlayerCombat2D : Damaging
{
  public PlayerMasterController playerMasterController;
  public Transform attackPoint;
  public LayerMask attackableLayers;       // remeber assigning layers
  public Stat attackTime;
  public Animator animator;
  protected float nextAttackTime = 0.3f;

  private void Update()
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

  public void startAttack()
  {
    animator.SetTrigger("Attack");
    dealDamage();
  }
  private void dealDamage()
  {
    Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.getValue(), attackableLayers);

    //If anything was hit, do some screen shake
    if (hitTargets.Length > 0)
    {
      CinemachineShake.Instance.ShakeCamera(2.5f, .3f);
    }

    // remember to give enemies 2D-Colliders        MD
    foreach (Collider2D target in hitTargets)
    {
      int targetLayer = target.gameObject.layer;
      if (targetLayer == LayerMask.NameToLayer("Destroyable") || targetLayer == LayerMask.NameToLayer("Attackable"))
      {
        Debug.Log("Hit Destroyable");
        target.GetComponent<Health>().applyDamage((int)attackPower.getValue());
      }
      else if (targetLayer == LayerMask.NameToLayer("Enemy"))
        target.GetComponent<KnockbackHealth>().getHit((int)attackPower.getValue(), this.gameObject); //Change KnockbackHealth back to EnemyHealth?
    }
  }

  private void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
      return;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange.getValue());
  }
}