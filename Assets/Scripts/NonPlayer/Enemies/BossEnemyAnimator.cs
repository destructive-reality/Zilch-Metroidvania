using UnityEngine;

public class BossEnemyAnimator : MonoBehaviour
{
  public bool isFacingRight = true;
  Animator animator;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    // animator.SetTrigger("RockSlide");
  }

  // Update is called once per frame
  void Update()
  {

  }
}
