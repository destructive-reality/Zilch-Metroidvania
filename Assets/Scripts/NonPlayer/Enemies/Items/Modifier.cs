﻿using UnityEngine;

public class Modifier : MonoBehaviour
{
  [SerializeField] private GameObject modifierEffect;
  private bool isInteractable = false;
  private Inventory playerInventory;

  [Header("Sounds")]
  public AudioClip pickupSound;
  public float pickupSoundVolume = 1f;


  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      isInteractable = true;
      playerInventory = other.GetComponentInChildren<Inventory>();
    }
  }

  private void Update()
  {
    if (isInteractable && Input.GetButtonDown("PickUp") && playerInventory)
    {
      AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, pickupSoundVolume);
      playerInventory.AddItem(modifierEffect);
      modifierEffect.transform.parent = playerInventory.gameObject.transform;
      Destroy(this.gameObject);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      isInteractable = false;
    }
  }
}
