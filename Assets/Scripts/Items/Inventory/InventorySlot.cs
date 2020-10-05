using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour//, IDropHandler
{
    public Image icon;
    public Button ItemButton;
    public int slotNumber;
    private GameObject modifier;

    public void AddItem(GameObject newModifier)
    {
        modifier = newModifier;

        icon.sprite = modifier.GetComponent<Effect>().modifier.icon;
        EnableInteraction(true);
    }

    public void ClearSlot()
    {
        modifier = null;

        icon.sprite = null;
        EnableInteraction(false);
    }

    public void EquipOnSlot(ModifierSlot slot)
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, slot);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(modifier, slot);
    }

    // public void OnDrop(PointerEventData eventData) {
    //     Debug.Log("Droped on Inventroy Slot " + slotNumber);
    //     Debug.Log("modifier is Equiped? " + eventData.pointerDrag.GetComponent<DragDrop>().isEquiped);
    //     if (eventData.pointerDrag != null)
    //     {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    //     }
    //     if (eventData.pointerDrag.GetComponent<DragDrop>().isEquiped)
    //     {
    //         Debug.Log("Item is equiped");
    //         // unequip this item with number from Equipment
    //     }
    // }
    private void EnableInteraction(bool value)
    {
        icon.enabled = value;

        ItemButton.interactable = value;
    }
}
