using System.Collections;
using UnityEngine;

public class FlyTowards : EnemyState
{
    private Vector3 target;
    private float speed;

    public FlyTowards(EnemyBehaviour _behaviour, Vector3 _target, float _speed) : base(_behaviour)
    {
        target = _target;
        speed = _speed;
    }

    public override IEnumerator Start()
    {
        behaviour.UseUpdate();
        yield break;
    }

    public override IEnumerator Update()
    {
        Debug.Log("Flying towards target");
        behaviour.transform.position = Vector2.MoveTowards(behaviour.transform.position, target, speed);
        
        yield return new WaitForFixedUpdate();
        behaviour.UseUpdate();
        yield break;
    }
}
