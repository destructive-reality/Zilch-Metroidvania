using UnityEngine;

public class FlyingEnemyAnimator : EnemyBehaviour {
    public Stat speed;
    private Vector3 startPosition;
    private Vector3 target;
    private Animator animator;

    public override void ResetState () { }

    public Vector3 GetStartPosition () {
        return startPosition;
    }

    private void Start () {
        startPosition = new Vector3 (transform.position.x, transform.position.y);
        animator = GetComponent<Animator> ();
    }

    private void Update () {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
        target = new Vector3 (playerPosition.x, playerPosition.y);

        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo (0);

        if ((currentAnimation.IsName ("FlyAround") || currentAnimation.IsName ("Wait")) &&
        Vector3.Distance (startPosition, target) <= 10f) {
            Debug.Log ("FlyingEnemy near Player");
            animator.SetBool ("isPlayerNear", true);

        }
    }
}