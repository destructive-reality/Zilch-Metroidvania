using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MovementsBase
{
    // Constructor for Attributes of the Parent
    public Patrol()
    {
        movingRight = true;
        speed = 3f;
    }

    public float rayDistance = 1f;
    public LayerMask ignoreLayers;

    public Transform groundDetection;

    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
    }

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
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }
}
