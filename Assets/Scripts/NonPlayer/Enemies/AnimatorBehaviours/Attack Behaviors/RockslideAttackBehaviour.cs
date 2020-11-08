using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockslideAttackBehaviour : StateMachineBehaviour
{
    private BossEnemyAnimator animatorScript;
    private BossCombat combatScript;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animatorScript = animator.GetComponent<BossEnemyAnimator>();
        combatScript = animator.GetComponent<BossCombat>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}