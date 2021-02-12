using UnityEngine;

public enum ATTACK_DIRECTION
{
  UP, DOWN, START
}

public class PlayerCombat2D : Damaging
{
  public PlayerMasterController playerMasterController;
  public Transform attackPoint;
  [SerializeField] private ParticleSystem attackParticles;
  public LayerMask attackableLayers;       // remeber assigning layers
  public Stat attackTime;
  public Animator playerAnimator;
  protected float nextAttackTime = 0.3f;
  public ATTACK_DIRECTION attackDirection = ATTACK_DIRECTION.START;

  [Header("Sounds")]
  public AudioClip attackSound;
  private AudioSource playerAudioSource;

  private void Awake()
  {
    playerAudioSource = GetComponent<AudioSource>();
  }

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
    switch (attackDirection)
    {
      case ATTACK_DIRECTION.UP:
        playerAnimator.SetTrigger("Attack Up");
        break;
      case ATTACK_DIRECTION.DOWN:
        playerAnimator.SetTrigger("Attack Down");
        break;
      case ATTACK_DIRECTION.START:
        playerAnimator.SetTrigger("Attack Normal");
        break;
      default:
        break;
    }
    dealDamage();
    if (playerAudioSource && attackSound)
    {
      playerAudioSource.PlayOneShot(attackSound);
    }
  }
  private void dealDamage()
  {
    Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange.getValue(), attackableLayers);

    //If anything was hit, do some screen shake
    if (hitTargets.Length > 0)
    {
      CinemachineShake.Instance.ShakeCamera(2.5f, .3f);
      attackParticles.Play();
    }

    foreach (Collider2D target in hitTargets)
    {
      if (target.isTrigger)
      {
        continue;
      }

      int targetLayer = target.gameObject.layer;
      if (targetLayer == LayerMask.NameToLayer("Destroyable") || targetLayer == LayerMask.NameToLayer("Attackable"))
      {
        // Debug.Log("Hit Destroyable");
        target.GetComponent<Health>().applyDamage((int)attackPower.getValue());
      }
      else if (targetLayer == LayerMask.NameToLayer("Enemy"))
        target.GetComponent<KnockbackHealth>().getHit((int)attackPower.getValue(), transform.position); //Change KnockbackHealth back to EnemyHealth?
    }
  }

  private void OnDrawGizmosSelected()
  {
    if (attackPoint == null)
      return;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange.getValue());
  }
}