using UnityEngine;

abstract public class KnockbackHealth : Health
{
    [Header("Sounds")]
    public AudioClip takingDamageSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public Stat knockbackForce;
    private Rigidbody2D rb2D;

    protected override void Start()
    {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public virtual void getHit(int damage, Vector2 damagingObjectPosition)
    {
        // Remove ApplyKnockback call here, since only Player uses it   MD
        applyKnockback(new Vector2(this.transform.position.x < damagingObjectPosition.x ? -knockbackForce.getValue() : knockbackForce.getValue(), knockbackForce.getValue() / 4));
        applyDamage(damage);

        //Only play getting hit sound if both audioSource and takingDamageSound are set
        if (audioSource && takingDamageSound)
        {
            audioSource.PlayOneShot(takingDamageSound);
        }
    }

    protected void applyKnockback(Vector2 knockbackForceVector)
    {
        // this.GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
        rb2D.AddForce(knockbackForceVector, ForceMode2D.Impulse);
    }
}