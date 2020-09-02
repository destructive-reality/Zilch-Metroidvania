using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBehaviour
{
    public Stat speed;
    public float flyAroundRange = 35f;
    private Vector3 startPosition;
    private Vector3 target;
    private float targetDistance;
    public void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y);
        SetState(new FlyAround(this, startPosition, flyAroundRange, speed.getValue()));
    }

    public void FixedUpdate()
    {
        if (state is Wait) return;

        if (target == new Vector3(0, 0, 0) && Vector3.Distance(startPosition, GameObject.FindGameObjectWithTag("Player").transform.position) <= 10f)
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            target = new Vector3(playerPosition.x, playerPosition.y);
            Debug.Log("FlyingEnemy notices Target");
        }

        if (target != null && state is FlyAround)
        {
            targetDistance = Vector3.Distance(this.transform.position, target);
            if (Vector3.Distance(startPosition, target) <= 12f)
            {
                Debug.Log("FlyingEnemy flies towards target");
                SetState(new FlyTowards(this, GameObject.FindGameObjectWithTag("Player").transform.position, speed.getValue()));
            }

        }

        if (state is FlyTowards)
        {
            targetDistance = Vector3.Distance(this.transform.position, target);
            if (targetDistance <= 1)
                SetState(new Wait(this));
        }
    }

    public override void ResetState()
    {
        target = new Vector3();
        SetState(new FlyAround(this, startPosition, flyAroundRange, speed.getValue()));
    }

}
