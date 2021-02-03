using UnityEngine;

public class PlayerHealth : KnockbackHealth
{
  public PlayerMasterController playerMasterController;
  public Stat startInvincibleTime;
  private float invincibleTime = 0f;
  public int lowLifePulseThreshold = 1;

  public override void setHealth(int healthToSetTo)
  {
    base.setHealth(healthToSetTo);
    UIController.Instance.setHealthInUI(currentHealth, (int)maxHealth.getValue());
    PostProController.Instance.SetLowLifePulseBool(currentHealth <= lowLifePulseThreshold ? true : false);
  }

  public new void getHit(int damage, Vector2 damagingObject)
  {
    if (Time.time >= invincibleTime)
    {
      base.getHit(damage, damagingObject);
      // applyKnockback(new Vector2(this.transform.position.x < damagingObject.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
      startInvincibilty();
      PostProController.Instance.TriggerChromaticAberrationDamageAnimation();
      CinemachineShake.Instance.ShakeCamera(2f, .1f);
      GameMaster.Instance.SlowdownTime(.6f, 1.5f);
    }
  }

  public void startInvincibilty()
  {
    invincibleTime = Time.time + startInvincibleTime.getValue();
  }

  protected override void die()
  {
    base.die();
    LevelLoader.Instance.ReloadCurrentLevel();
    GetComponent<PlayerMovement>().enabled = false;
    GetComponent<PlayerCombat2D>().enabled = false;
    playerMasterController.playerMovementController.playerAnimator.SetTrigger("Death");
    Debug.Log(gameObject.name + " is dying. Bye!");
  }
}