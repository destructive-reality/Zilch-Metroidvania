using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AreaHider : MonoBehaviour
{
  [SerializeField] private Light2D hidingLight;
  [SerializeField] private float fadeDuration;
  [SerializeField] private float targetIntensity;
  // [SerializeField] private AudioSource audioSource;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      // audioSource.PlayOneShot(audioSource.clip);
      // hidingLight.enabled = false;
      StartCoroutine("fadeLight");
      gameObject.GetComponent<Collider2D>().enabled = false;
    }
  }

  private IEnumerator fadeLight()
  {
    float currentTime = 0;

    while (currentTime < fadeDuration)
    {
      currentTime += Time.deltaTime;
      // hidingLight.volumeOpacity = Mathf.Lerp(hidingLight.volumeOpacity, 0, currentTime / fadeDuration);
      hidingLight.intensity = Mathf.Lerp(hidingLight.intensity, targetIntensity, currentTime / fadeDuration);

      yield return null;
    }
    hidingLight.enabled = false;
    yield break;
  }

}