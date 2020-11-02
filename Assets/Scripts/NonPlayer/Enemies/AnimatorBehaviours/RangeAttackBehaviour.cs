using UnityEngine;

public class RangeAttackBehaviour : StateMachineBehaviour
{
    [SerializeField] private Transform prefabProjectile;
    private MeleeHorizontalMovingEnemy animatorScript;
    private MeleeEnemyCombat meleeCombatScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animatorScript = animator.GetComponent<MeleeHorizontalMovingEnemy>();
        meleeCombatScript = animator.GetComponent<MeleeEnemyCombat>();
        Vector2 dir = Vector2.right;
        dir.x = animatorScript.isFacingRight ? 1 : -1;

        Vector2 origin = new Vector2(animator.transform.position.x, meleeCombatScript.attackPoint.position.y);
        Transform tsfProjectile = Instantiate(prefabProjectile, origin, Quaternion.identity);
        tsfProjectile.GetComponent<Projectile>().Setup(dir);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
        
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
