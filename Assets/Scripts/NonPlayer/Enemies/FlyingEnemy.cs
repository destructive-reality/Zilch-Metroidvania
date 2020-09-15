using UnityEngine;

public class FlyingEnemy : EnemyBehaviour {
    public Stat speed;
    public float flyAroundRange;
    private Vector3 startPosition;
    private Vector3 target;
    private float targetDistance;

    public override void ResetState () {
        target = Vector3.zero;
        targetDistance = 0;
        SetState (new FlyAround (this, startPosition, flyAroundRange, speed.getValue ()));
    }

    private void Start () {
        startPosition = new Vector3 (transform.position.x, transform.position.y);
        SetState (new FlyAround (this, startPosition, flyAroundRange, speed.getValue ()));
    }

    private void FixedUpdate () {
        StartCoroutine (state.FixedUpdate ());
    }

    private void Update () {
        StartCoroutine (state.Update ());

        Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
        if (target == Vector3.zero && Vector3.Distance (startPosition, playerPosition) <= 10f) {
            target = new Vector3 (playerPosition.x, playerPosition.y);
            Debug.Log ("FlyingEnemy notices Target");
        }

        if (target != Vector3.zero && (state is FlyReset || state is FlyAround)) {
            if (Vector3.Distance (startPosition, target) <= 10f) {
                Debug.Log ("FlyingEnemy flies towards target");
                target = new Vector3 (playerPosition.x, playerPosition.y);
                targetDistance = Vector3.Distance (this.transform.position, target);
                SetState (new FlyTowards (this, target, speed.getValue ()));
            }
        }

        if (state is FlyTowards) {
            targetDistance = Vector3.Distance (this.transform.position, target);
            if (targetDistance <= 1f)
                SetState (new Wait (this));

        }
    }
}