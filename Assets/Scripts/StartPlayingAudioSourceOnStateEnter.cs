using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayingAudioSourceOnStateEnter : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<AudioSource>().Play();
    }
}
