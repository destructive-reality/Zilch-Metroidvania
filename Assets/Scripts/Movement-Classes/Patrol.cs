using UnityEngine;

public class Patrol : MovementsBase
{
    public float rayDistance = 1f;
    public LayerMask ignoreLayers;

    public Transform groundDetection;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, ~ignoreLayers);
        if (groundInfo.collider == null)
        {
            Flip();
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
