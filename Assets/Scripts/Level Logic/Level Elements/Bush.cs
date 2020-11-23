using System.Collections.Generic;
using UnityEngine;

//Required Components
[RequireComponent(typeof(AudioSource))]
public class Bush : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource bushAudioSource;
    public List<AudioClip> bushWalkThroughAudioClips;

    [Header("Particles")]
    public ParticleSystem bushWalkThroughParticleSystem;

    [Header("Animation")]
    public Animator bushAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bushAudioSource.PlayOneShot(bushWalkThroughAudioClips[Random.Range(0, bushWalkThroughAudioClips.Count)]);
            bushWalkThroughParticleSystem.Play();
            // bushAnimator.SetTrigger("WalkThrough");
        }
    }
}
