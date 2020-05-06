using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit(int damage)
    {
        Debug.Log("get Hit");
        currentHealth -= damage;
        // Animation goes here
        // animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Animation goes here
        // animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        this.enabled = false;
    }
}
