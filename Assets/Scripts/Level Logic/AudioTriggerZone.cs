using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class AudioTriggerZone : MonoBehaviour
{
    public string triggeringTag;
    public bool triggerOnlyOnce = true;
    private bool hasAlreadyBeenTriggered = false;
    private AudioSource triggerZoneAudioSource;

    void Awake()
    {
        triggerZoneAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((!triggerOnlyOnce || !hasAlreadyBeenTriggered) && other.CompareTag(triggeringTag))
        {
            triggerZoneAudioSource.Play();
            hasAlreadyBeenTriggered = true;
        }
    }
}
