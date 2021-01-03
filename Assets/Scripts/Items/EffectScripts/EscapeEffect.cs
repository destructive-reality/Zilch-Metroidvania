using UnityEngine;

public class EscapeEffect : Effect
{
  private PlayerMovement playerMovement;
  [SerializeField] private float wallSlidingSpeedReduction;
  [SerializeField] private float xJumpForce;
  [SerializeField] private float yJumpForce;
  [SerializeField] private int iFrameTimeBoost = 1;

  public bool IsWallJumpable
  {
    get;
    private set;
  }
  public override void ArmStart(bool value)
  {
    Debug.Log("Reduce Players Wall Sliding Speed: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.wallSlidingSpeed.addModifier(-wallSlidingSpeedReduction);
    else
      playerMovement.wallSlidingSpeed.removeModifier(-wallSlidingSpeedReduction);
  }
  public override void LegStart(bool value)
  {
    Debug.Log("Give Player WallJump Jump: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    IsWallJumpable = false;
  }
  public override void WeaponStart(bool value)
  {
    Debug.Log("Increase Player Invincible Time: " + value);
    PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
    if (value)
      playerHealth.startInvincibleTime.addModifier(iFrameTimeBoost);
    else
      playerHealth.startInvincibleTime.removeModifier(iFrameTimeBoost);
  }
  public override void HeadStart(bool value)
  {
    Debug.Log("Give Player I-Frames on Dash: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
    if (value)
      playerMovement.DashActions.AddListener(playerHealth.startInvincibilty);
    else
      playerMovement.DashActions.RemoveListener(playerHealth.startInvincibilty);
  }
  public override void ArmUpdate()
  {

  }
  public override void LegUpdate()
  {
    if (playerMovement.isAirborne() && playerMovement.getWallSliding())
    {
      IsWallJumpable = true;
      if (Input.GetButtonDown("Jump"))
      {
        Debug.Log("Execute Wall Jump");
        int direction = 0;
        if (playerMovement.isFacingRight)
        {
          direction = -1;
        }
        else
        {
          direction = 1;
        }
        playerMovement.jumpForce.addModifier(yJumpForce);
        playerMovement.ApplyJumpForce(direction * xJumpForce);
        playerMovement.jumpForce.removeModifier(yJumpForce);
      }
    }
    else
      IsWallJumpable = false;
  }
  public override void WeaponUpdate()
  {

  }
  public override void HeadUpdate()
  {

  }
}