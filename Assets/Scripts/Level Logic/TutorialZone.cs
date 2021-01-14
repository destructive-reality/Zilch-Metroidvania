using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class TutorialZone : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D spotlightLight;
    // public float lightFadeDuration;
    private AudioSource audioSource;
    private bool lightOn = false;

    private bool soundAlreadyPlayed = false;

    // private float startLightIntensity;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // startLightIntensity = spotlightLight.intensity;
        // spotlightLight.intensity = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spotlightLight.enabled = true;
            if (!soundAlreadyPlayed)
            {
                audioSource.Play();
                soundAlreadyPlayed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spotlightLight.enabled = false;
        }
    }

    // private void Update()
    // {
    //     if (lightOn)
    //     {
    //         spotlightLight.intensity = Mathf.Lerp(0, startLightIntensity, lightFadeDuration);
    //     }
    //     else
    //     {
    //         spotlightLight.intensity = Mathf.Lerp(startLightIntensity, 0, lightFadeDuration);
    //     }
    // }
}
