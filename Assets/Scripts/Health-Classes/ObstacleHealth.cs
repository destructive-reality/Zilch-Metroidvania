using UnityEngine;

public class ObstacleHealth: Health 
{
    protected override void die()
    {
        base.die();
        Destroy(this.gameObject);
    }
}