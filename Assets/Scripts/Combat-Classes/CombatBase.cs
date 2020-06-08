using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBase : Damaging
{
    public float attackTime;
    protected float nextAttackTime = 0f;
}
