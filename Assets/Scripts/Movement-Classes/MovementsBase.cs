using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    public bool isFacingRight = true;
    public Stat speed;

    public void flip()
    {
        isFacingRight = !isFacingRight;

        //TODO kann man das nicht ohne zwischenspeichern direkt flippen?
        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }

    public void flipRight()
    {
        isFacingRight = true;

        Vector3 localScale = transform.localScale;
        if (localScale.x < 0)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    public void flipLeft()
    {
        isFacingRight = false;

        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }
}
