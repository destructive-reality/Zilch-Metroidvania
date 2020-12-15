using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public ParticleSystem healParticleSystem;

    public AudioClip healSound;
    private AudioSource audioSource;


    public Animator animator;
    private bool isInteractable = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isInteractable && Input.GetButtonDown("PickUp")) {
            healParticleSystem.Play();
            audioSource.PlayOneShot(healSound);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().healToMaxHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
            animator.SetBool("isActive", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
            animator.SetBool("isActive", false);
        }
    }

    void healPlayer()
    {
        healParticleSystem.Play();
    }
}
