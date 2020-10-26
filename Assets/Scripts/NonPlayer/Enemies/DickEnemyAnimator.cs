using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DickEnemyAnimator : HorizontalMovingEnemy
{

    public Transform[] patrolPointTransforms;
    private float aggresionRange;
    private List<Vector2> patrolPoints;

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public List<Vector2> getPatrolPoints()
    {
        return patrolPoints;
    }

    private void Start()
    {
        patrolPoints = new List<Vector2>();
        foreach (Transform transform in patrolPointTransforms)
        {
            patrolPoints.Add(new Vector2(transform.position.x, transform.position.y));
        }
        // foreach (Transform transform in patrolPointTransforms)  // Might as well delete uneccessary GOs
        // {
        //     GameObject.Destroy(transform.gameObject);
        // }

    }

    private void Update()
    {
        // Draw raycast to look for Player in facing-direction when not attacking
    }

    private void FixedUpdate()
    {

    }


}