using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IchusHealth : KnockbackHealth
{
    protected override void die()
    {
        base.die();
        GetComponent<Animator>().SetTrigger("death");
    }
}
