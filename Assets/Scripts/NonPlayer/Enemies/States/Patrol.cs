using System.Collections;
using UnityEngine;

public class Patrol : EnemyState {

    private float speed;
    private Transform groundDetection;
    private float rayDistance = 1f;
    private LayerMask moveOnLayer;

    public Patrol (EnemyBehaviour _behaviour, float _speed, Transform _groundDetector) : base (_behaviour) {
        speed = _speed;
        groundDetection = _groundDetector;
        moveOnLayer = LayerMask.GetMask("Ground");
        // Debug.Log(groundDetection.gameObject.name);
    }

    public override IEnumerator Update () {
        base.Update ();
        RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, rayDistance, moveOnLayer);
        if (groundInfo.collider == null) {
            // Debug.Log("PatrollingEnemy needs to flip");
            behaviour.Flip ();
        }
        yield break;
    }

    public override IEnumerator FixedUpdate () {
        base.FixedUpdate();
        if (behaviour.isFacingRight) {
            behaviour.transform.Translate (Vector2.right * speed * Time.deltaTime);
        } else {
            behaviour.transform.Translate (Vector2.left * speed * Time.deltaTime);
        }
        yield break;
    }
}