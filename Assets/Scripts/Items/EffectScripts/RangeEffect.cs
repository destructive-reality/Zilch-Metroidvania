using UnityEngine;

public class RangeEffect : Effect
{
  private float attackRangeBoost = 0.3f;
  private float dashTimeBoost = 0.2f;
  public override void ArmStart(bool value = true)
  {

  }
  public override void LegStart(bool value = true)
  {
    Debug.Log("Increase Player Dash Time: " + value);
    PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.startDashTime += dashTimeBoost;
    else
      playerMovement.startDashTime -= dashTimeBoost;
  }
  public override void WeaponStart(bool value = true)
  {
    Debug.Log("Increase Player Attack Range: " + value);
    PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    if (value)
      playerCombat.attackRange.addModifier(attackRangeBoost);
    else
      playerCombat.attackRange.removeModifier(attackRangeBoost);
  }
  public override void HeadStart(bool value = true)
  {

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
