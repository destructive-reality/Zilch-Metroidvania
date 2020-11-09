using UnityEngine;

public class Projectile : MonoBehaviour     // vielleicht von EnemyCombat erben lassen, da die OnTriggerEnter auch Schaden verursacht??
{
    protected Vector3 direction;
    protected float velocity;

    public void Setup(Vector2 _direction, float _timeToExist = 3f, float _velocity = 5f)
    {
        direction = _direction;
        if (direction.x == -1)
        {
            transform.Rotate(0, 0, 180);
        }
        velocity = _velocity;
        Destroy(gameObject, _timeToExist);
    }

    // private void FixedUpdate()
    // {
    //     transform.position += direction * velocity * Time.deltaTime;
    // }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.getHit(1, this.gameObject);
            Destroy(gameObject);
        }
    }
}
