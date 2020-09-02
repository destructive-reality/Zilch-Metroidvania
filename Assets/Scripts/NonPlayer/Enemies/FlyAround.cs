using System.Collections;
using UnityEngine;

public class FlyAround : EnemyState
{
    private Vector2 startPosition;
    private float range;
    private float speed;

    public FlyAround(EnemyBehaviour _behaviour, Vector2 _position, float _range, float _speed) : base(_behaviour)
    {
        startPosition = new Vector2(_position.x, _position.y);
        range = _range;
        speed = _speed;
        Debug.Log("Start flying around");
    }

    public override IEnumerator Start()
    {
        behaviour.UseUpdate();
        yield break;
    }

    public override IEnumerator Update()
    {
        Debug.Log("Flying towards start position");
        behaviour.transform.position = Vector2.MoveTowards(behaviour.transform.position, startPosition, speed);

        yield return new WaitForFixedUpdate();
        behaviour.UseUpdate();
        yield break;
    }
}
