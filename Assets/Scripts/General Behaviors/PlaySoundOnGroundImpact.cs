using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnGroundImpact : StateMachineBehaviour
{
  public AudioClip soundToPlay;
  public bool interruptPlayingSound = false;
  private AudioSource audioSource;
  private bool isSoundPlayed;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    audioSource = animator.GetComponent<AudioSource>();
    isSoundPlayed = false;
  }
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
  {
    if (isSoundPlayed)
    {
      return;
    }
    RaycastHit2D hitDown = Physics2D.Raycast(animator.transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
    if (hitDown.collider != null)
    {
      if (soundToPlay)
      {
        audioSource.PlayOneShot(soundToPlay);
      }
      isSoundPlayed = true;
    }
  }

}
