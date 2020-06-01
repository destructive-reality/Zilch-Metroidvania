using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableHealth : Health
{
    public override void getHit(int damage)
    {
        onHit();
        base.getHit(damage);
    }
    protected virtual void onHit()
    {
        // nothing at first
    }
}
