using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateTo : MovementsBase
{
    public LevitateTo()
    {
        movingRight = true;
        speed = 0;
    }

    public Transform target;
    protected Vector3 headingTarget;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // for disctance between enemy and target
            headingTarget = target.position - this.transform.position;
            // float targetDistance = headingTarget.magnitude;
            // float targetAngle = Vector3.Angle(this.transform.forward, headingTarget);
            if (headingTarget.x > 0 && !movingRight)
            {
                Flip();
            }
            if (headingTarget.x < 0 && movingRight)
            {
                Flip();
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(headingTarget * 0.1f * Time.deltaTime, Space.World);
    }
}
