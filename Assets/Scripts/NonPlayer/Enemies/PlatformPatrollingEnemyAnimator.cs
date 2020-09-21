using UnityEngine;

public class PlatformPatrollingEnemyAnimator : EnemyBehaviour {
    public Stat speed;
    public Transform groundDetector;
    private Animator animator;
    public override void ResetState () {
        // SetState (new Patrol (this, speed.getValue (), groundDetector));
        // return;
    }
    private void Start () {
        animator = GetComponent<Animator>();
        // Debug.Log ("Start patrolling.");
        // state = new Patrol (this, speed.getValue (), groundDetector);
    }
    // private void Update () {
    //     StartCoroutine (state.Update ());
    // }
    // private void FixedUpdate () {
    //     StartCoroutine (state.FixedUpdate ());
    // }
}