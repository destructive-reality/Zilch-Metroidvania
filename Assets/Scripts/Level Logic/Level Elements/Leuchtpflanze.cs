using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Leuchtpflanze : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.position.x > this.transform.position.x) {
            animator.SetTrigger("bendToLeft");
        }
        else {
            animator.SetTrigger("bendToRight");
        }
    }
}
