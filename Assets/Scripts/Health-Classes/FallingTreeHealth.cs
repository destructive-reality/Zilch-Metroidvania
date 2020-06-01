using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTreeHealth : Health
{

    Rigidbody2D rb;
    Vector2 hitForce = Vector2.right * 1500;
    public Transform hitPoint;
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void onHit()
    {
        if (true)
        {
            Debug.Log("Tree got hit!");
            rb.AddForceAtPosition(hitForce, hitPoint.position);
        }
    }
}