﻿using UnityEngine;

public class InventorySlot : Slot
{
  private Inventory inventory;

  private void Start()
  {
    inventory = GameObject.FindWithTag("Player").GetComponentInChildren<Inventory>();
  }

  public override void AddItem(GameObject _newModifier)
  {
    base.AddItem(_newModifier);
  }

  public override void ClearSlot()
  {
    if (item)
      inventory.UseItem(item);

    base.ClearSlot();
  }
}