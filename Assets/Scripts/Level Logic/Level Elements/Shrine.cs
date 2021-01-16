using UnityEngine;

public class Shrine : MonoBehaviour
{
  [SerializeField] private ParticleSystem healParticleSystem;

  [SerializeField] private AudioClip healSound;
  private AudioSource audioSource;

  [SerializeField] private Animator animator;
  protected bool isInteractable = false;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    if (isInteractable && Input.GetButtonDown("PickUp"))
    {
      healParticleSystem.Play();
      audioSource.PlayOneShot(healSound);
      GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().healToMaxHealth();
    }
  }

  protected virtual void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      isInteractable = true;
      animator.SetBool("isActive", true);
    }
  }

  protected virtual void OnTriggerExit2D(Collider2D other)
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
