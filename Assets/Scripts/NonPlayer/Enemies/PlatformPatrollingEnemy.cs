using UnityEngine;

public class PlatformPatrollingEnemy : EnemyBehaviour {
    public Stat speed;
    public Transform groundDetector;
    public override void ResetState () {
        SetState (new Patrol (this, speed.getValue (), groundDetector));
        return;
    }
    private void Start () {
        Debug.Log ("Start patrolling.");
        state = new Patrol (this, speed.getValue (), groundDetector);
    }
    private void Update () {
        StartCoroutine (state.Update ());
    }
    private void FixedUpdate () {
        StartCoroutine (state.FixedUpdate ());
    }
}