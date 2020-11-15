using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStateEnter : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public bool interruptPlayingSound = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audioSource = animator.GetComponent<AudioSource>();

        if (interruptPlayingSound)
        {
            audioSource.Stop();
        }

        //Play sound once
        if (soundToPlay)
        {
            audioSource.PlayOneShot(soundToPlay);
        }

    }
}
