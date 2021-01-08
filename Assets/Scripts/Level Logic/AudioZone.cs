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
      fadeTo(initialVolume, lightAdjustment);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      fadeTo(0f);
    }
  }

  private void fadeTo(float volume, float _lightIntensity = 0)
  {
    if (currentCoroutine != null)
    {
      StopCoroutine(currentCoroutine);
    }
    if (_lightIntensity == 0)
      currentCoroutine = startFade(fadeDuration, volume);
    else
      currentCoroutine = startFade(fadeDuration, volume, _lightIntensity);
    StartCoroutine(currentCoroutine);
  }

  private IEnumerator startFade(float duration, float targetVolume, float _targetLightIntensity = 0)
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
      if (_targetLightIntensity > 0)
      {
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, _targetLightIntensity, currentTime / duration);
      }
      yield return null;
    }
    yield break;
  }
}