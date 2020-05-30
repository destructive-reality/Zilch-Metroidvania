﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    public ModifierObject modifier;
    bool isInteractable = false;
    Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
            playerInventory = other.GetComponent<Inventory>();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isInteractable && Input.GetButtonDown("PickUp") && playerInventory)
        {
            playerInventory.AddModifier(modifier);
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
