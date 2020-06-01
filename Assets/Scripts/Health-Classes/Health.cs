using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected Animator animator;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void setHealth(int healthToSetTo)
    {
        if (healthToSetTo < 0)
        {
            Debug.LogError("New health cannot be below zero.");
        }
        if (healthToSetTo > maxHealth)
        {
            Debug.LogError("New health cannot be above max health.");
        }
        currentHealth = healthToSetTo;
    }

    public virtual void applyDamage(int damageToTake)
    {
        Debug.Log(gameObject.name + " gets Hit for " + damageToTake);
        setHealth(Mathf.Clamp(currentHealth - damageToTake, 0, maxHealth));

        if (isDead())
        {
            die();
        }
    }

    public void heal(int amountToHeal)
    {
        Debug.Log(gameObject.name + " heals for " + amountToHeal);
        setHealth(Mathf.Clamp(currentHealth + amountToHeal, 0, maxHealth));
    }

    protected virtual void die()
    {
        Debug.Log(gameObject.name + " is dying. Bye!");
    }

    public bool isDead() {
        return currentHealth <= 0;
    }
}
