using UnityEngine;

public class EnemyHealth : KnockbackHealth
{
    protected override void die()
    {
        base.die();
        Destroy(this.gameObject);
    }
}