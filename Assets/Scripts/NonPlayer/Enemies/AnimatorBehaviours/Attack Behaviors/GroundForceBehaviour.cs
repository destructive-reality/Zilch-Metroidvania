using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForceBehaviour : StateMachineBehaviour
{
  [SerializeField] private Transform groundForcePrefab;
  [SerializeField] private float groundForceWidth;
  private BossEnemyAnimator animatorScript;
  private BossCombat combatScript;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    animatorScript = animator.GetComponent<BossEnemyAnimator>();
    combatScript = animator.GetComponent<BossCombat>();

    for (int i = 0; i < combatScript.groundForceSpikesCount; i++)
    {
      Vector2 nextSpikePosition = new Vector2(combatScript.groundForceAttackPoint.position.x + (i * groundForceWidth), combatScript.groundForceAttackPoint.position.y);
      createSpike(nextSpikePosition);
      Debug.Log("createSpike called");
    }
  }

  private void createSpike(Vector2 _position)
  {
    Debug.Log("Create Spike");
    Transform tsfGroundAttack = Instantiate(groundForcePrefab, _position, Quaternion.identity);
    tsfGroundAttack.GetComponent<GroundForce>().Setup(6, 1);
    Debug.Log("Spike created");
  }
}