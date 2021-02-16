using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AudioZone : MonoBehaviour
{
  private AudioSource audioSource;
  private float initialVolume;
  private IEnumerator currentCoroutine;
  [SerializeField] private Light2D globalLight;
  [SerializeField] private float lightAdjustment = 0.5f;
  [SerializeField] private float fadeDuration;

  void Awake()
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
      //   audioSource.time = Random.Range(0f, audioSource.clip.length);
      if (!audioSource.isPlaying)
      {
        audioSource.time = 0f;
      }

      fadeTo(initialVolume, lightAdjustment);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      fadeTo();
    }
  }

  private void fadeTo(float _volume = 0, float _lightIntensity = 0)
  {
    if (currentCoroutine != null)
    {
      StopCoroutine(currentCoroutine);
    }
    if (_lightIntensity == 0)
      currentCoroutine = startFade(fadeDuration, _volume);
    else
      currentCoroutine = startFade(fadeDuration, _volume, _lightIntensity);
    StartCoroutine(currentCoroutine);
  }

  private IEnumerator startFade(float _duration, float _targetVolume = 0, float _targetLightIntensity = 0)
  {
    float currentTime = 0;
    float start = audioSource.volume;

    if (_targetVolume != 0 && !audioSource.isPlaying)
    {
      audioSource.Play();
    }

    while (currentTime < _duration)
    {
      currentTime += Time.deltaTime;
      audioSource.volume = Mathf.Lerp(start, _targetVolume, currentTime / _duration);
      if (_targetVolume <= 0f && audioSource.volume <= 0f)
      {
        audioSource.Stop();
        Debug.Log("Stopping Audio source from audiozone");
      }
      if (_targetLightIntensity > 0)
      {
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, _targetLightIntensity, currentTime / _duration);
      }
      yield return null;
    }
    yield break;
  }
}