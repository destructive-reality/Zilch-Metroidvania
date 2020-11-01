using UnityEngine;

public class PatrolMovement : MovementsBase
{
    public float rayDistance = 1f;
    public LayerMask ignoreLayers;

    public Transform groundDetection;

    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, ~ignoreLayers);
        if (groundInfo.collider == null)
        {
            flip();
        }
    }
    private void FixedUpdate()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed.getValue() * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed.getValue() * Time.deltaTime);
        }

    }
}
