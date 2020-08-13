using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTreeHealth : AttackableHealth
{
    Rigidbody2D rb;
    Vector2 hitForce = Vector2.right * 1500;
    public Transform hitPoint;
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    protected override void onHit()
    {
        if (currentHealth == (int) maxHealth.getValue())
        {
            Debug.Log("Tree got hit!");
            rb.AddForceAtPosition(hitForce, hitPoint.position);
        }
    }
}