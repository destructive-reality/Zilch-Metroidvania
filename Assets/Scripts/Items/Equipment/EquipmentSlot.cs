using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public Image[] icons;
    public Button ItemButton;
    private EquipmentUI equipmentUI;
    private GameObject modifier;

    public void AddItem(GameObject newModifier)
    {
        modifier = newModifier;

        icons[0].sprite = modifier.GetComponent<Effect>().modifier.icon;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = true;
        }
        ItemButton.interactable = true;
    }

    public void ClearSlot()
    {
        modifier = null;

        icons[0].sprite = null;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = false;
        }
        ItemButton.interactable = false;
    }

    public void EquipOnSlot(ModifierSlot slot)
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, slot);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(modifier, slot);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        switch (equipmentUI.checkSlot(this.gameObject))
        {
            case null:
                Debug.Log("No corresponding Slot found!");
                return;
            case 0:     // Head
                return;
            case 1:     // Arm
                return;
            case 2:     // Arm
                return;
            case 3:     // Leg
                return;
            case 4:     // Leg
                return;
            default:
                Debug.Log("Body");
                return;
        }

    }

    private void Start() {
        equipmentUI = GameObject.FindWithTag("UI").GetComponentInChildren<EquipmentUI>();
        if (!equipmentUI)
        {
            Debug.LogError("No Equipment UI found for Equipment Slot!");
        }
    }
}
