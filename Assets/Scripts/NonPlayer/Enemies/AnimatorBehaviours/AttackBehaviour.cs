using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private Transform attackPoint;
    private float attackPower;
    private float attackRange;
    private MeleeHorizontalMovingEnemy animatorScript;
    private MeleeEnemyCombat combatScript;
    private float frameCountForTesting;     // Change to point in animation to inflict damage
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        frameCountForTesting = 0;

        combatScript = animator.GetComponent<MeleeEnemyCombat>();
        animatorScript = animator.GetComponent<MeleeHorizontalMovingEnemy>();

        attackPoint = combatScript.attackPoint;
        attackPower = combatScript.attackPower.getValue();
        attackRange = combatScript.attackRange.getValue();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        frameCountForTesting++;
        Debug.Log(animatorScript.DistanceToPlayer(attackPoint.position));
        if (frameCountForTesting == 80 && animatorScript.DistanceToPlayer(attackPoint.position) <= attackRange)
        {
            Debug.Log("Try Attack from AttackBehaviour");
            combatScript.Attack();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.ResetTrigger("inAttackRange");
        // Debug.Log(frameCountForTesting);
    }
}