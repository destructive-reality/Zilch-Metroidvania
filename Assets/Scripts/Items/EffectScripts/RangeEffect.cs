using UnityEngine;

public class RangeEffect : Effect
{
  PlayerMovement playerMovement;
  private float attackRangeBoost = 0.3f;
  private float dashTimeBoost = 0.2f;
  private PlayerCombat2D playerCombat;
  public Transform projectilePrefab;
  private float rangeAttackCooldown = 1f;
  private float rangeAttackTimer = 0;

  public override void ArmStart(bool value = true)
  {

  }
  public override void LegStart(bool value = true)
  {
    Debug.Log("Increase Player Dash Time: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.startDashTime += dashTimeBoost;
    else
      playerMovement.startDashTime -= dashTimeBoost;
  }
  public override void WeaponStart(bool value = true)
  {
    Debug.Log("Increase Player Attack Range: " + value);
    playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    if (value)
      playerCombat.attackRange.addModifier(attackRangeBoost);
    else
      playerCombat.attackRange.removeModifier(attackRangeBoost);
  }
  public override void HeadStart(bool value = true)
  {
    Debug.Log("Enable Range Attack: " + value);
    playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();

  }
  public override void ArmUpdate()
  {

  }
  public override void LegUpdate()
  {

  }
  public override void WeaponUpdate()
  {

  }
  public override void HeadUpdate()
  {
    if (Time.time >= rangeAttackTimer && Input.GetButtonDown("Shoot"))
    {
      Debug.Log("Shoot");
      rangeAttackTimer = Time.time + rangeAttackCooldown;
      FireProjectile();
    }
  }
  private void FireProjectile()
  {
    Vector2 dir = Vector2.right;
    dir.x = playerMovement.isFacingRight ? 1 : -1;

    Vector2 origin = playerCombat.attackPoint.position;
    Transform tsfProjectile = Instantiate(projectilePrefab, origin, Quaternion.identity);
    tsfProjectile.GetComponent<Projectile>().Setup(dir, true, 0.7f);
  }
}