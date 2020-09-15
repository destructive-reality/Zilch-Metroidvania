using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    protected bool movingRight = true;
    public Stat speed;

    protected void Flip()
    {
        movingRight = !movingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}
