using System.Collections;
using UnityEngine;

public class Wait : EnemyState
{
    public Wait(EnemyBehaviour _behaviour) : base(_behaviour)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        behaviour.ResetState();
        yield break;
    }
}
