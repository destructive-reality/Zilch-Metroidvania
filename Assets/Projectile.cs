using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float velocity;

    public void Setup(Vector2 _direction, float _velocity = 5f)
    {
        direction = _direction;
        if (direction.x == -1)
        {
            transform.Rotate(0, 0, 180);
        }
        velocity = _velocity;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += direction * velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.getHit(1, this.gameObject);
            Destroy(gameObject);
        }
    }
}
