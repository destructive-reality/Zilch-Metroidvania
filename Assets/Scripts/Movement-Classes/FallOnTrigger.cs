using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTrigger : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && rb.velocity.y < 0)
        {
            Damaging.DealDamage(gameObject, damage, other.gameObject);
            // other.gameObject.GetComponent<PlayerHealth>().getHit(10, gameObject);
        }
    }
}
