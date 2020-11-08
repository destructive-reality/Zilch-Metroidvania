using UnityEngine;

public class HorizontalProjectile : Projectile
{

    private void FixedUpdate()
    {
        transform.position += direction * velocity * Time.deltaTime;
    }
}
