using UnityEngine;

public class InventorySlot : Slot
{
    public void EquipOnSlot(ModifierSlot slot)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(item, slot);
    }
}