using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class AudioTriggerZone : MonoBehaviour
{
    public string triggeringTag;

    [Tooltip("Only trigger the first time an object with the triggering tag enters this zone?")]
    public bool triggerOnlyOnce = true;

    [Tooltip("Should all other AudioTriggerZones be stopped when this one is triggered?")]
    public bool stopAllOtherAudioTriggerZones = true;
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
            if (stopAllOtherAudioTriggerZones)
            {
                foreach (AudioTriggerZone otherAudioTriggerZone in FindObjectsOfType<AudioTriggerZone>())
                {
                    otherAudioTriggerZone.triggerZoneAudioSource.Stop();
                }
            }
            triggerZoneAudioSource.Play();
            hasAlreadyBeenTriggered = true;


        }
    }
}
