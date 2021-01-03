using UnityEngine;

public class PowerEffect : Effect
{
  [SerializeField] private int attackPowerBoost = 1;
  [SerializeField] private int dashSpeedBoost = 15;
  [SerializeField] private int healthBoost = 1;
  [SerializeField] private float jumpTimeBoost = 1;
  public override void ArmStart(bool value)
  {
    Debug.Log("Increase Player JumpTime: " + value);
    PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.jumpTime += jumpTimeBoost;
    else
      playerMovement.jumpTime -= jumpTimeBoost;
  }
  public override void LegStart(bool value)
  {
    Debug.Log("Increase Player DashSpeed: " + value);
    PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.dashSpeed.addModifier(dashSpeedBoost);
    else
      playerMovement.dashSpeed.removeModifier(dashSpeedBoost);
  }
  public override void WeaponStart(bool value)
  {
    Debug.Log("Increase Player Damage: " + value);
    PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    if (value)
      playerCombat.attackPower.addModifier(attackPowerBoost);
    else
      playerCombat.attackPower.removeModifier(attackPowerBoost);
  }
  public override void HeadStart(bool value)
  {
    Debug.Log("Increase Player Health: " + value);
    PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
    if (value)
      playerHealth.maxHealth.addModifier(healthBoost);
    else
      playerHealth.maxHealth.removeModifier(healthBoost);
    playerHealth.setHealth((int)playerHealth.maxHealth.getValue());
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

  }
}
