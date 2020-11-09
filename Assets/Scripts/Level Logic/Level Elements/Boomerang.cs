using UnityEngine;

public class Boomerang : Projectile
{
    [SerializeField] private float yVelocity;
    private Vector3 startPosition;
    private float multiplier;
    private BoomerangState state;

    public void Setup(Vector2 _direction, float _timeToLife = 9f, float _velocity = 10f, float _yVelocity = -2)
    {
        base.Setup(_direction, _timeToLife, _velocity);

        yVelocity = _yVelocity;
    }

    private void Start()
    {
        startPosition = transform.position;
        multiplier = 1 / (3 * 1 / Time.fixedDeltaTime);
        state = BoomerangState.FlyForward;
    }

    private void FixedUpdate()
    {
        if (state == BoomerangState.FlyForward)
        {
            transform.position += new Vector3(direction.x * 0.9f, yVelocity * multiplier) * velocity * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.forward, 200 * multiplier);

            if (transform.position.y < (startPosition.y + yVelocity) && yVelocity < 0 || transform.position.y > (startPosition.y + yVelocity) && yVelocity > 0)
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

    enum BoomerangState
    {
        FlyForward,
        Rotate,
        FlyBack
    }
}