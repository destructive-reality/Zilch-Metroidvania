using UnityEngine;

public class PlatformPatrollingEnemyAnimator : HorizontalMovingEnemy
{
  private Animator animator;
  public override void ResetState()
  {
  }
  private void Start()
  {
    animator = gameObject.GetComponent<Animator>();
  }

  //   void OnTriggerEnter2D(Collider2D _collider)
  //   {
  //     Debug.Log("collider triggered");
  //     if (_collider.CompareTag("Ground"))
  //     {
  //       flip();
  //     }
  //   }
}