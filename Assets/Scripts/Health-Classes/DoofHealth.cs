using UnityEngine;

public class DoofHealth : KnockbackHealth
{
    protected override void die()
    {
        base.die();
        GetComponent<Animator>().SetTrigger("death");
    }
}
