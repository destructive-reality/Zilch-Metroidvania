using UnityEngine;

public class RangeEffect : Effect
{
  [SerializeField] private float hoverTime = 1;
  private float hoverTimeCounter = 0;
  private ParticleSystem jumpParticles;
  private PlayerMovement playerMovement;
  private float attackRangeBoost = 0.3f;
  private float dashCooldownReduction = -1;
  private PlayerCombat2D playerCombat;
  public Transform projectilePrefab;
  private float rangeAttackCooldown = 1f;
  private float rangeAttackTimer = 0;

  public override void ArmStart(bool value = true)
  {
    Debug.Log("Grant Player hover after jump: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    jumpParticles = playerMovement.jumpParticles;
    if (value)
      playerMovement.JumpTimeEnds.AddListener(hdlHover);
    else
      playerMovement.JumpTimeEnds.RemoveListener(hdlHover);
  }
  public override void LegStart(bool value = true)
  {
    Debug.Log("Decreases Player Dash cooldown: " + value);
    playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    if (value)
      playerMovement.startDashCooldown.addModifier(dashCooldownReduction);
    else
      playerMovement.startDashCooldown.removeModifier(dashCooldownReduction);

    // if (value)
    //   playerMovement.startDashTime += dashTimeBoost;
    // else
    //   playerMovement.startDashTime -= dashTimeBoost;
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
    if (hoverTimeCounter > 0)
    {
      Debug.Log("Check hoverTimer");
      if (!jumpParticles.isPlaying || jumpParticles.time > jumpParticles.main.duration / 2)
      {
        jumpParticles.Stop();
        jumpParticles.Play();
      }
      if (hoverTimeCounter < Time.time || Input.GetButtonUp("Jump"))
      {
        playerMovement.playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        hoverTimeCounter = 0;
        jumpParticles.Stop();
      }
      if (Input.GetButton("Jump") && hoverTimeCounter > Time.time && playerMovement.playerRigidbody2D.velocity.y < -0.5f)
      {
        playerMovement.playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        jumpParticles.Play();
      }
    }
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

  private void hdlHover()
  {
    if (hoverTimeCounter == 0)
    {
      Debug.Log("Set hover time");
      hoverTimeCounter = hoverTime + Time.time;
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