using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour//, IDropHandler
{
    public Image[] icons;
    public Button ItemButton;
    public Button ArmButton;
    public Button LegButton;
    public Button BodyButton;
    public int slotNumber;
    private GameObject modifier;

    public void AddItem(GameObject newModifier)
    {
        modifier = newModifier;

        icons[0].sprite = modifier.GetComponent<Effect>().modifier.icon;
        EnableInteraction(true);
    }

    public void ClearSlot()
    {
        modifier = null;

        icons[0].sprite = null;
        EnableInteraction(false);
    }

    public void OnArmButton()
    {
        // Debug.Log("ArmButton clicked");
        EquipOnSlot(ModifierSlot.Arm);
    }
    public void OnLegButton()
    {
        // Debug.Log("LegButton clicked");
        EquipOnSlot(ModifierSlot.Leg);
    }
    public void OnBodyButton()
    {
        // Debug.Log("BodyButton clicked");
        EquipOnSlot(ModifierSlot.Weapon);
    }
    public void EquipOnSlot(ModifierSlot slot) {
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
    private void EnableInteraction(bool value) {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = value;
        }
        ItemButton.interactable = ArmButton.interactable = BodyButton.interactable = LegButton.interactable = value;
    }
}
