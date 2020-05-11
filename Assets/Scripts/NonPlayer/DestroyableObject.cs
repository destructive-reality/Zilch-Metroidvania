using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;

    private Color firstColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        firstColor = GetComponent<MeshRenderer>().material.color;
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
        flashRed();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void flashRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("setbackColor", 0.3f);
        // yield return new WaitForSeconds(0.3f);   // didnt work in IEnuerator
        // GetComponent<MeshRenderer>().material.color = firstColor;
    }

    void setbackColor()
    {
        GetComponent<MeshRenderer>().material.color = firstColor;
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
