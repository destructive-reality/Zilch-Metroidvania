using UnityEngine;

public class RangeAttackBehaviour : StateMachineBehaviour
{
    [SerializeField] protected Transform prefabProjectile;
    protected MeleeHorizontalMovingEnemy animatorScript;
    protected MeleeEnemyCombat combatScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animatorScript = animator.GetComponent<MeleeHorizontalMovingEnemy>();
        combatScript = animator.GetComponent<MeleeEnemyCombat>();
        
        FireProjectile(animator.transform);
    }

    protected void FireProjectile(Transform _trfAnimator) {
        Vector2 dir = Vector2.right;
        Debug.Log(animatorScript);
        dir.x = animatorScript.isFacingRight ? 1 : -1;

        Vector2 origin = new Vector2(_trfAnimator.position.x, combatScript.attackPoint.position.y);
        Transform tsfProjectile = Instantiate(prefabProjectile, origin, Quaternion.identity);
        tsfProjectile.GetComponent<Projectile>().Setup(dir);
    }
}