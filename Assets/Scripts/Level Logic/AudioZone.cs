using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private AudioSource audioSource;
    private float initialVolume;
    private IEnumerator currentCoroutine;

    public float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        audioSource.volume = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.time = Random.Range(0f, audioSource.clip.length);

            audioSource.Play();
            fadeTo(initialVolume);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fadeTo(0f);
        }
    }

    private void fadeTo(float volume)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = startFade(fadeDuration, volume);
        StartCoroutine(currentCoroutine);
    }

    private IEnumerator startFade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            if (targetVolume <= 0f && audioSource.volume <= 0f)
            {
                audioSource.Stop();
            }
            yield return null;
        }
        yield break;
    }
}
