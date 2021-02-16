using UnityEngine;

public class PlayerHealth : KnockbackHealth
{
    public PlayerMasterController playerMasterController;
    public Stat startInvincibleTime;
    private float invincibleTime = 0f;
    public int lowLifePulseThreshold = 1;

    [Header("Sounds")]
    public AudioClip takeDamageAudioClip;
    [Range(0f, 4f)]
    public float takeDamageAudioClipVolume = 1f;

    public AudioClip deathAudioClip1;
    [Range(0f, 4f)]
    public float deathAudioClip1Volume = 1f;
    public AudioClip deathAudioClip2;
    [Range(0f, 4f)]
    public float deathAudioClip2Volume = 1f;
    private AudioSource playerAudioSource;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

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
            if (takeDamageAudioClip)
            {
                playerAudioSource.PlayOneShot(takeDamageAudioClip, takeDamageAudioClipVolume);
            }
        }
    }

    public void startInvincibilty()
    {
        invincibleTime = Time.time + startInvincibleTime.getValue();
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
        LevelLoader.Instance.ReloadCurrentLevel();
    }
}