using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    public bool isFacingRight = true;
    public Stat speed;

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}
