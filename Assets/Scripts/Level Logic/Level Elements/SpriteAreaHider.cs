using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAreaHider : MonoBehaviour
{
  [SerializeField] private SpriteRenderer hiderSprite;
  [SerializeField] private float alphaReduction = 0.02f;
  // [SerializeField] private AudioSource audioSource;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      // audioSource.PlayOneShot(audioSource.clip);
      StartCoroutine("fadeHider");
      gameObject.GetComponent<Collider2D>().enabled = false;
    }
  }

  private IEnumerator fadeHider()
  {
    Color workingColor = hiderSprite.color;

    while (hiderSprite.color.a >= 0)
    {
      workingColor.a -= alphaReduction;
      hiderSprite.color = workingColor;
      yield return null;
    }

    Destroy(gameObject);
    yield break;
  }
}