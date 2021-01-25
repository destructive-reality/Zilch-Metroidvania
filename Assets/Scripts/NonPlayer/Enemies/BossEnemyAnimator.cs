using UnityEngine;

public enum ACTION
{
  RAPID_FIRE = 0, BOOMERANG_FIRE = 1, ROCK_SLIDE = 2, GROUND_FORCE = 3
}

public class BossEnemyAnimator : Flipables
{
  static string[] actions = new string[] { "RapidFire", "BoomerangFire", "RockSlide", "GroundForce" };
  private Animator animator;
  [SerializeField] private BossHealth bossHealth;

  private void Start()
  {
    animator = GetComponent<Animator>();
    bossHealth.reachHealtBreakpoint.AddListener(hdlBreakpoint);
  }

  private void hdlBreakpoint()
  {
    animator.SetTrigger(actions[(int)ACTION.GROUND_FORCE]);
  }
}