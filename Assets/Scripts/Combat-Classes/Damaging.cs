using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damaging : MonoBehaviour
{
    public Animator animator;
    public Stat attackPower;
    public Stat attackRange;

    public static void DealDamage(GameObject source, int damage, GameObject target)
    {
        if (target.GetComponent<PlayerHealth>())
        {
            target.GetComponent<PlayerHealth>().getHit(damage, source);
        }
        else if (target.GetComponent<KnockbackHealth>())
        {
            target.GetComponent<KnockbackHealth>().getHit(damage, source);
        }
        else
        {
            target.GetComponent<Health>().applyDamage(damage);
        }
    }
}
