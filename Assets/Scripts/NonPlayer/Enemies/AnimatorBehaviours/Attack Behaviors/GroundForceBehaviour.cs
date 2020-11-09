using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForceBehaviour : StateMachineBehaviour
{
    [SerializeField] private Transform prefabGameObject;
    private BossEnemyAnimator animatorScript;
    private BossCombat combatScript;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animatorScript = animator.GetComponent<BossEnemyAnimator>();
        combatScript = animator.GetComponent<BossCombat>();

        Transform tsfGroundAttack = Instantiate(prefabGameObject, combatScript.rangeAttackPoint.position, Quaternion.identity);
        tsfGroundAttack.GetComponent<GroundForce>().Setup(6, 1);
    }

    // public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    // {
    // }

    // public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    // {
    // }
}