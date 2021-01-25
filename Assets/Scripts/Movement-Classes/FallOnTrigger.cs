using UnityEngine;

public class FallOnTrigger : MonoBehaviour
{
  public int damage = 1;
  Rigidbody2D rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Player"))
    {
      rb.isKinematic = false;
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Player") && rb.velocity.y < -0.1f)
    {
      Debug.Log(rb.velocity.y);
      // Damaging.DealDamage(gameObject, damage, other.gameObject);
      other.gameObject.GetComponent<PlayerHealth>().getHit(damage, transform.position);
    }
  }
}
