using UnityEngine;

public class HorizontalProjectile : Projectile
{
  [SerializeField] private ParticleSystem partSys;

  private void FixedUpdate()
  {
    transform.position += direction * velocity * Time.deltaTime;
  }

  protected override void explode(float _timeToExist = 0)
  {
    partSys.Play();
    Destroy(gameObject, partSys.main.duration + _timeToExist);

    GetComponent<Collider2D>().enabled = false;
    Destroy(GetComponent<Rigidbody2D>());
    GetComponentInChildren<SpriteRenderer>().enabled = false;
    this.enabled = false;
  }
}