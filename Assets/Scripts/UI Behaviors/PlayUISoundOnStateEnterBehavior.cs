using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUISoundOnStateEnterBehavior : StateMachineBehaviour
{
    public AudioClip soundToPlay;

    [Range(0.0f, 4.0f)]
    public float audioVolume = 1f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UIController.Instance)
        {
            UIController.Instance.getUiAudioSource().PlayOneShot(soundToPlay, audioVolume);
        }
        else
        {
            AudioSource.PlayClipAtPoint(soundToPlay, Camera.main.transform.position, audioVolume);
        }

    }

}