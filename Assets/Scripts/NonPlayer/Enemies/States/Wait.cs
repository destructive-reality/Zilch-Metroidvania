using System.Collections;
using UnityEngine;

public class Wait : EnemyState
{

    public Wait(EnemyBehaviour _behaviour) : base(_behaviour)
    {
    }

    public override IEnumerator Start()
    {
        base.Start();
        yield return new WaitForSeconds(2);
        behaviour.ResetState();

        yield break;
    }
}
