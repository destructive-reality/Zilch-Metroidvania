using UnityEngine;

public class DestroyBehaviour : StateMachineBehaviour
{
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Destroy(animator.gameObject);
  }
}