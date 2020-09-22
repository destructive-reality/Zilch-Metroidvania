using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAround : EnemyState
{
    private Vector2 startPosition;
    private Vector2 target;
    private float range;
    private float speed;

    public FlyAround(EnemyBehaviour _behaviour, Vector2 _startPosition, float _range, float _speed) : base(_behaviour)
    {
        startPosition = _startPosition;
        range = _range;
        speed = _speed;
    }

    public override IEnumerator Start()
    {
        base.Start();
        target = getTarget(startPosition, range);
        yield break;
    }

    public override IEnumerator Update()
    {
        base.Update();
        if (targetDistance() <= 1f)
        {
            behaviour.SetState(new Wait(behaviour));
        }
        yield break;
    }

    public override IEnumerator FixedUpdate()
    {
        base.Update();
        behaviour.transform.position = Vector2.MoveTowards(behaviour.transform.position, target, speed);
        yield break;
    }

    public float targetDistance()
    {
        return Vector2.Distance(target, behaviour.transform.position);
    }

    private Vector2 getTarget(Vector2 _position, float _maxRange)
    {
        Vector2 target = _position;

        float angle = Random.Range(0, 2 * Mathf.PI);
        float range = Random.Range(0, _maxRange);
        Vector2 randomV = new Vector2(range * Mathf.Sin(angle), range * Mathf.Cos(angle));

        // Debug.Log("Random Vector is " + randomV);
        // Debug.Log("Distance is " + Vector2.Distance(target, target + randomV));
        target = target + randomV;
        // Debug.Log("Target is " + target);

        return target;
    }
}
