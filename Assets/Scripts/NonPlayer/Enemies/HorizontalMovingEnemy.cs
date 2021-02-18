using System.Collections.Generic;
using UnityEngine;

public abstract class HorizontalMovingEnemy : EnemyBehaviour
{
  public Transform groundDetector;
  [Header("Sounds")]
  public List<AudioClip> walkingAudioClips;
  [Range(0f, 4f)] public float walkingAudioVolume = 1;

  [SerializeField] private AudioSource enemyAudioSource;


  public void triggerRandomWalkSoundClip()
  {
    enemyAudioSource.PlayOneShot(walkingAudioClips[Random.Range(0, walkingAudioClips.Count)], walkingAudioVolume);
  }
}