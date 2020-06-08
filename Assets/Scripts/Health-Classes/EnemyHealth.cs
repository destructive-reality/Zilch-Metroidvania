using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : KnockbackHealth
{
    protected override void die()
    {
        base.die();
        Destroy(this.gameObject);
    }
}
