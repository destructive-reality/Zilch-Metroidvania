using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBase : Damaging
{
    public Stat attackTime;
    protected float nextAttackTime = 0.3f;
}
