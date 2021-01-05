using UnityEngine;

public class RapidFireBehaviour : StateMachineBehaviour
{
  [SerializeField] protected Transform prefabProjectile;
  [SerializeField] protected float projectileLifeTime = 3;
  [SerializeField] protected float projectileYOriginAdjustment = 0;
  [SerializeField] protected float projectileVelocity = 10;
  [SerializeField] protected int[] fireFrames;
  protected int currentFrame = 0;
  protected BossEnemyAnimator animatorScript;
  protected BossCombat combatScript;
  // private 


  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animatorScript = animator.GetComponent<BossEnemyAnimator>();
    combatScript = animator.GetComponent<BossCombat>();
  }

  public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    foreach (int i in fireFrames)
    {
      if (i == currentFrame)
      {
        FireProjectile(animator.transform);
      }
    }
    currentFrame++;
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    currentFrame = 0;
  }

  protected void FireProjectile(Transform _trfAnimator)
  {
    Vector2 dir = Vector2.right;
    dir.x = animatorScript.isFacingRight ? 1 : -1;

    Vector2 origin = combatScript.rangeAttackPoint.position;
    origin.y += projectileYOriginAdjustment;
    Transform tsfProjectile = Instantiate(prefabProjectile, origin, Quaternion.identity);
    tsfProjectile.GetComponent<Projectile>().Setup(dir, false, projectileLifeTime, projectileVelocity);
  }

}