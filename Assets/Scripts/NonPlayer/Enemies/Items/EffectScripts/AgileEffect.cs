using UnityEngine;

enum ATTACK_DIRECTION
{
  UP, DOWN, START
}

public class AgileEffect : Effect
{
  [SerializeField] private float attackSpeedBoost = -0.1f;
  private PlayerMovement playerMovement;
  private bool isDoublejumped;
  public int knockbackForceReduction;
  private Transform attackPoint;
  private Vector2 attackPointStartPosition;
  private ATTACK_DIRECTION attackDir = ATTACK_DIRECTION.START;

  public override void ArmStart(bool value)
  {
    Debug.Log("Increase Player Attack Speed: " + value);
    PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    if (value)
      playerCombat.attackTime.addModifier(attackSpeedBoost);
    else
      playerCombat.attackTime.removeModifier(attackSpeedBoost);
  }
  public override void LegStart(bool value)
  {
    Debug.Log("Give Player Double Jump: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    isDoublejumped = false;
  }
  public override void WeaponStart(bool value)
  {
    Debug.Log("Decrease Player Knockback: " + value);
    PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
    if (value)
      playerHealth.knockbackForce.addModifier(-knockbackForceReduction);
    else
      playerHealth.knockbackForce.removeModifier(-knockbackForceReduction);
  }
  public override void HeadStart(bool value)
  {
    Debug.Log("Let Player move AttackPoint: " + value);
    PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
    attackPoint = playerCombat.attackPoint;
    attackPointStartPosition = new Vector2(attackPoint.position.x, attackPoint.position.y);
  }
  public override void ArmUpdate()
  {

  }
  public override void LegUpdate()
  {
    if (isDoublejumped && !playerMovement.isAirborne())
    {
      isDoublejumped = !isDoublejumped;
    }
    if (playerMovement.isAirborne() && Input.GetButtonDown("Jump") && !isDoublejumped)
    {
      if (Object.FindObjectOfType<EscapeEffect>().IsWallJumpable)
      {
        Debug.Log("Use Walljump instead of Double Jump");
        return;
      }
      Debug.Log("Execute Double Jump");
      playerMovement.ApplyJumpForce();
      isDoublejumped = true;
    }
  }
  public override void WeaponUpdate()
  {

  }
  public override void HeadUpdate()
  {
    if (Input.GetAxis("Vertical") != 0)
    {
      if (Input.GetButtonDown("Up") && attackDir == ATTACK_DIRECTION.START)
      {
        attackDir = ATTACK_DIRECTION.UP;
        attackPoint.Translate(-1, 1.5f, 0, Space.Self);
      }
      if (Input.GetButtonDown("Down") && attackDir == ATTACK_DIRECTION.START)
      {
        attackDir = ATTACK_DIRECTION.DOWN;
        attackPoint.Translate(-1, -1.5f, 0, Space.Self);
      }
      if (Input.GetButtonUp("Up") && attackDir == ATTACK_DIRECTION.UP)
      {
        attackDir = ATTACK_DIRECTION.START;
        attackPoint.Translate(1, -1.5f, 0, Space.Self);
      }
      if (Input.GetButtonUp("Down") && attackDir == ATTACK_DIRECTION.DOWN)
      {
        attackDir = ATTACK_DIRECTION.START;
        attackPoint.Translate(1, 1.5f, 0, Space.Self);
      }
    }
  }
}