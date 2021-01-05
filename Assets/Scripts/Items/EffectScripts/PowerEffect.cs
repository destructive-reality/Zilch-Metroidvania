using UnityEngine;

public class PowerEffect : Effect
{
  private PlayerMovement playerMovement;
  private PlayerCombat2D playerCombat;
  [SerializeField] private int dashSpeedBoost = 15;
  [SerializeField] private float jumpTimeBoost = 1;
  [SerializeField] private int attackPowerBoost = 1;
  [SerializeField] private int healthBoost = 1;
  public override void ArmStart(bool value)
  {
    Debug.Log("Deal damage while dashing: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    if (value)
      playerMovement.CollisionAction.AddListener(dashCollision);
    else
      playerMovement.CollisionAction.AddListener(dashCollision);
  }
  public override void LegStart(bool value)
  {
    Debug.Log("Increase Player JumpTime: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.jumpTime += jumpTimeBoost;
    else
      playerMovement.jumpTime -= jumpTimeBoost;
  }
  public override void WeaponStart(bool value)
  {
    Debug.Log("Increase Player Damage: " + value);
    playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
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

  private void dashCollision()
  {
    if (playerMovement.GetState() == State.Dashing)
    {
      playerCombat.startAttack();
    }
  }
}