using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementsBase : MonoBehaviour
{
    protected bool movingRight;
    public float speed;

    //TODO hier vllt lieber den spriterenderer flippen? BH
    protected void Flip()
    {
        movingRight = !movingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}
