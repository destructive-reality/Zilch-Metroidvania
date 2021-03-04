using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : KnockbackHealth
{
  public PlayerMasterController playerMasterController;
  public Stat startInvincibleTime;
  private float invincibleTime = 0f;
  public int lowLifePulseThreshold = 1;

  [Header("Sounds")]
  public List<AudioClip> takeDamageAudioClipCollection;
  [Range(0f, 4f)]
  public float takeDamageAudioVolume = 1f;

  public AudioClip deathAudioClip1;
  [Range(0f, 4f)]
  public float deathAudioClip1Volume = 1f;
  public AudioClip deathAudioClip2;
  [Range(0f, 4f)]
  public float deathAudioClip2Volume = 1f;
  private AudioSource playerAudioSource;

  [Header("Respawning")]
  public Shrine lastUsedShrine;
  public UnityEvent onDeath;

  private void Awake()
  {
    playerAudioSource = GetComponent<AudioSource>();

    onDeath = new UnityEvent();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Keypad0))
    {
      this.setHealth(0);
    }
  }

  public override void setHealth(int healthToSetTo)
  {
    base.setHealth(healthToSetTo);
    UIController.Instance.setHealthInUI(currentHealth, (int)maxHealth.getValue());
    PostProController.Instance.SetLowLifePulseBool(currentHealth <= lowLifePulseThreshold ? true : false);
  }

  public new void getHit(int damage, Vector2 damagingObject)
  {
    if (Time.unscaledTime >= invincibleTime)
    {
      startInvincibilty();
      Debug.Log("Player taking damage");
      base.getHit(damage, damagingObject);
      // applyKnockback(new Vector2(this.transform.position.x < damagingObject.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
      PostProController.Instance.TriggerChromaticAberrationDamageAnimation();
      CinemachineShake.Instance.ShakeCamera(2f, .1f);
      GameMaster.Instance.SlowdownTime(.6f, 1.5f);
      if (takeDamageAudioClipCollection.Count > 0)
      {
        playerAudioSource.PlayOneShot(takeDamageAudioClipCollection[Random.Range(0, takeDamageAudioClipCollection.Count)], takeDamageAudioVolume);
      }
    }
  }

  public void startInvincibilty()
  {
    invincibleTime = Time.unscaledTime + startInvincibleTime.getValue();
  }

  protected override void die()
  {
    base.die();
    if (deathAudioClip1)
    {
      playerAudioSource.PlayOneShot(deathAudioClip1, deathAudioClip1Volume);
    }
    if (deathAudioClip2)
    {
      playerAudioSource.PlayOneShot(deathAudioClip2, deathAudioClip2Volume);
    }
    GetComponent<PlayerMovement>().enabled = false;
    GetComponent<PlayerCombat2D>().enabled = false;
    playerMasterController.playerMovementController.playerAnimator.SetTrigger("Death");
    Debug.Log(gameObject.name + " is dying. Bye!");
    onDeath.Invoke();

    float deathDelayTime = 4f;

    //Make player invincible to prevent multiple death triggers
    invincibleTime = Time.unscaledTime + deathDelayTime + 2f;

    // LevelLoader.Instance.ReloadCurrentLevelAfterSeconds(4f);
    Invoke("respawn", deathDelayTime);
  }

  private void respawn()
  {
    GetComponent<PlayerMovement>().enabled = true;
    GetComponent<PlayerCombat2D>().enabled = true;
    healToMaxHealth();
    playerMasterController.playerMovementController.playerAnimator.SetTrigger("Respawn");
    transform.position = lastUsedShrine.transform.position;
  }
}