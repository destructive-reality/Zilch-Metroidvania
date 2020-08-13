using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    protected bool movingRight = true;
    public Stat speed;

    //TODO hier vllt lieber den spriterenderer flippen? BH -> 8.6.20: Ja bitte, das ist ziemlich dumm. BH
    protected void Flip()
    {
        movingRight = !movingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}
