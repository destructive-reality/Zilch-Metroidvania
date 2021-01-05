using UnityEngine;

public class Projectile : MonoBehaviour     // vielleicht von EnemyCombat erben lassen, da die OnTriggerEnter auch Schaden verursacht??
{
  protected Vector3 direction;
  protected float velocity;
  private bool isFriendly;

  public void Setup(Vector2 _direction, bool _isFriendly = false, float _timeToExist = 3f, float _velocity = 7f)
  {
    direction = _direction;
    if (direction.x == -1)
    {
      transform.Rotate(0, 0, 180);
    }
    isFriendly = _isFriendly;
    velocity = _velocity;
    Destroy(gameObject, _timeToExist);
  }

  // private void FixedUpdate()
  // {
  //     transform.position += direction * velocity * Time.deltaTime;
  // }

  protected void OnTriggerEnter2D(Collider2D other)
  {
    KnockbackHealth health;
    Debug.Log(other.gameObject.layer);
    if (!isFriendly)
    {
      Debug.Log("Hit Player, isFriendly " + isFriendly);
      health = other.GetComponent<PlayerHealth>();
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Destroyable"))
    {
      Debug.Log("Hit Enemy");
      health = other.GetComponent<KnockbackHealth>();
    }
    else
      health = null;
    if (health != null)
    {
      health.getHit(1, this.gameObject);
      Destroy(gameObject);
    }
  }
}
