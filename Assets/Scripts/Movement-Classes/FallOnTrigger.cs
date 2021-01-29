using UnityEngine;

public class FallOnTrigger : MonoBehaviour
{
  public int damage = 1;
  [SerializeField] private Collider2D triggerCollider;
  [SerializeField] private Rigidbody2D rb;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Player"))
    {
      rb.isKinematic = false;
      triggerCollider.enabled = false;
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