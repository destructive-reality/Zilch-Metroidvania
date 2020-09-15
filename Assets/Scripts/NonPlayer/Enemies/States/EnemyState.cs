using System.Collections;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyBehaviour behaviour;

    public EnemyState(EnemyBehaviour _behaviour)
    {
        behaviour = _behaviour;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Update()
    {
        yield break;
    }
    
    public virtual IEnumerator FixedUpdate()
    {
        yield break;
    }
}
