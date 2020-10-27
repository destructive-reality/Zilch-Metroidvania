using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprintDemo : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("Death");
            rigidbody2D.AddTorque(15f, ForceMode2D.Impulse);
            rigidbody2D.AddForce(new Vector2(12 * transform.localScale.x, 0), ForceMode2D.Impulse);
            rigidbody2D.gravityScale = 1f;
        }
    }
}
