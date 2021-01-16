using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicShuffler : MonoBehaviour
{
    public float interTrackDelay = 1f;
    public AudioClip[] audioClipsToPlay;

    private AudioClip lastPlayedClip;
    private AudioSource audioSource;

    private float interTrackTimer = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = selectRandomAudioClip();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            interTrackTimer += Time.unscaledDeltaTime;
        }

        if (interTrackTimer >= interTrackDelay)
        {
            audioSource.clip = selectRandomAudioClip();
            audioSource.Play();
            interTrackTimer = 0;
        }
    }

    AudioClip selectRandomAudioClip()
    {
        AudioClip newAudioClip = audioClipsToPlay[Random.Range(0, audioClipsToPlay.Length)];

        if (audioClipsToPlay.Length <= 1 && newAudioClip == lastPlayedClip)
        {
            newAudioClip = audioClipsToPlay[Random.Range(0, audioClipsToPlay.Length)];
        }

        lastPlayedClip = newAudioClip;
        return newAudioClip;
    }
}
