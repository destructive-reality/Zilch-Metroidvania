using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] protected int damage;
  protected Vector3 direction;
  protected float velocity;
  private bool isFriendly;

  public void Setup(Vector2 _direction, bool _isFriendly = false, float _timeToExist = 3f, float _velocity = 7f, int _damage = 1)
  {
    direction = _direction;
    if (direction.x == -1)
    {
      transform.Rotate(0, 0, 180);
    }
    isFriendly = _isFriendly;
    velocity = _velocity;
    damage = _damage;
    Destroy(gameObject, _timeToExist);
    // explode(_timeToExist);
  }

  protected void OnTriggerEnter2D(Collider2D other)
  {
    if (!isFriendly)
    {
      if (other.CompareTag("Player"))
      {
        other.GetComponent<PlayerHealth>().getHit(damage, this.gameObject.transform.position);
        explode();
      }
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    {
      other.GetComponent<KnockbackHealth>().getHit(damage, this.gameObject.transform.position);
      explode();
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Destroyable") || other.gameObject.layer == LayerMask.NameToLayer("Attackable"))
    {
      other.GetComponent<Health>().applyDamage(damage);
      explode();
    }
    if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
      explode();
    }
  }

  protected virtual void explode(float _timeToExist = 0)
  {
    Destroy(gameObject, _timeToExist);
  }
}