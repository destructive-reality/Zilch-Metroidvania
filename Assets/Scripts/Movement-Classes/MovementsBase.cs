using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    protected bool movingRight = true;
    public Stat speed;

    //Flip the Sprite (Default: Right)
    public void flip()
    {
        movingRight = !movingRight;

        //TODO kann man das nicht ohne zwischenspeichern direkt flippen?
        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }

    public void flipRight()
    {
        movingRight = true;

        Vector3 localScale = transform.localScale;
        if (localScale.x < 0)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    public void flipLeft()
    {
        movingRight = false;

        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }
}
