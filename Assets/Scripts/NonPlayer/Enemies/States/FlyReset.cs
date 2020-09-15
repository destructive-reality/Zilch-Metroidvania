using System.Collections;
using UnityEngine;

public class FlyReset : EnemyState
{
    private Vector2 startPosition;
    private float speed;

    public FlyReset(EnemyBehaviour _behaviour, Vector2 _position, float _speed) : base(_behaviour)
    {
        startPosition = new Vector2(_position.x, _position.y);
        speed = _speed;
        Debug.Log("Start flying around");
    }

    public override IEnumerator FixedUpdate()
    {
        base.Update();
        // Debug.Log("Flying towards start position");
        behaviour.transform.position = Vector2.MoveTowards(behaviour.transform.position, startPosition, speed);

        yield break;
    }
}
