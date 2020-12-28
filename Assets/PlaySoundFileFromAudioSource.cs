using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFileFromAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playAudioClipFromAudioSource(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
