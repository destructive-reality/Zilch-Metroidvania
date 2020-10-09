using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofEnemyAnimator : EnemyBehaviour
{
    [SerializeField] private float aggressionRange;
    private Vector2 startPosition;
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, aggressionRange);
    }
}
