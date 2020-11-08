using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private float yVelocity;
    private Vector3 startPosition;
    private Vector3 direction;
    private float velocity;
    private float multiplier;
    private BoomerangState state;

    public void Setup(Vector2 _direction, float _velocity = 10f, float _yVelocity = -2)
    {
        direction = _direction;
        if (direction.x == -1)
        {
            transform.Rotate(0, 0, 180);
        }
        velocity = _velocity;
        yVelocity = _yVelocity;
        Destroy(gameObject, 9f);
    }

    private void Start()
    {
        startPosition = transform.position;
        multiplier = 1 / (3 * 1 / Time.fixedDeltaTime);
        state = BoomerangState.FlyForward;
        // Setup(Vector2.left);
    }

    private void FixedUpdate()
    {
        if (state == BoomerangState.FlyForward)
        {
            transform.position += new Vector3(direction.x * 0.9f, yVelocity * multiplier) * velocity * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.forward, 200 * multiplier);

            if (transform.position.y < startPosition.y + yVelocity && yVelocity < 0 || transform.position.y > startPosition.y + yVelocity && yVelocity > 0)
            {
                yVelocity = 0;
                direction.x *= -1;
                state = BoomerangState.Rotate;
            }
            else
                multiplier += multiplier * 0.03f;
        }
        if (state == BoomerangState.FlyBack)
        {
            transform.position += direction * velocity * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.forward, 190 * multiplier);
            multiplier += multiplier * 0.01f;
        }
        if (state == BoomerangState.Rotate)
        {
            hdlRotation();
        }
    }

    private void hdlRotation()
    {
        transform.RotateAround(transform.position, Vector3.forward, 180 * multiplier);
        if (multiplier > 0)
        {
            multiplier += -multiplier * 0.03f;
            if (multiplier < 0.01f)
            {
                multiplier = -multiplier;

            }
        }
        else if (multiplier < 0)
        {
            multiplier += multiplier * 0.02f;
            if (multiplier < -0.1f)
            {
                state = BoomerangState.FlyBack;
            }
        }
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

    enum BoomerangState
    {
        FlyForward,
        Rotate,
        FlyBack
    }
}