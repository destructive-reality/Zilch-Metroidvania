using UnityEngine;

public class LevitateTo : MovementsBase
{
    public Transform target;
    protected Vector3 headingTarget;
    private bool isActive = false;
    
    // Update is called once per frame
    void Update()
    {
        if (target && isActive)
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
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed.getValue());
        // transform.Translate(headingTarget * 0.1f * Time.deltaTime, Space.World);
    }
}
