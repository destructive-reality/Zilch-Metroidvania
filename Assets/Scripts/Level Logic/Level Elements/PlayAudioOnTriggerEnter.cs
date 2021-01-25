using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    public AudioClip audioClip;

    public string tagName;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
