using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    protected bool movingRight = true;
    public Stat speed;

    //hier vllt lieber den spriterenderer flippen? BH -> 8.6.20: Ja bitte, das ist ziemlich dumm. BH -> 15.9.20: Doch nicht so dumm, xD. MD (basically BH)
    protected void Flip()
    {
        movingRight = !movingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}
