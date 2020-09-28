using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image[] icons;
    public Button ItemButton;
    public ModifierSlot slotPosition;
    private int slotNumber;
    private EquipmentUI equipmentUI;
    private GameObject modifier;

    public void InitializeEquipmentSlotPosition(int i)
    {
        slotNumber = i;
        switch (i)
        {
            case 0:
                slotPosition = ModifierSlot.Head;
                break;
            case 1:
            case 2:
                slotPosition = ModifierSlot.Arm;
                break;
            case 3:
            case 4:
                slotPosition = ModifierSlot.Leg;
                break;
            default:
                slotPosition = ModifierSlot.Body;
                break;
        }
        // icons[0].GetComponent<DragDrop>().isEquiped = true;
    }
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

    public void ChangeSlotOf(int modifierSlotNumber)
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(modifier, slot);
        Debug.Log("Initiate modifier slot change");
        equipmentUI.StartSlotChange(modifierSlotNumber, slotPosition);
    }

    // public void OnDrop(PointerEventData eventData)
    // {
    //     if (eventData.pointerDrag != null)
    //     {
    //         eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    //     }
    //     // int modifierSlotNumber = ;
    //     switch (equipmentUI.checkSlot(this.gameObject))
    //     {
    //         case null:
    //             Debug.Log("No corresponding Slot found!");
    //             return;
    //         case 0:     // Head
    //             EquipOnSlot(ModifierSlot.Head);
    //             return;
    //         case 1:     // Arm
    //         case 2:     // Arm
    //             EquipOnSlot(ModifierSlot.Arm);
    //             return;
    //         case 3:     // Leg
    //         case 4:     // Leg
    //             EquipOnSlot(ModifierSlot.Leg);
    //             return;
    //         default:
    //             EquipOnSlot();
    //             Debug.Log("Body");
    //             return;
    //     }
    // }

    public GameObject GetModifier()
    {
        if (modifier)
            return modifier;
        else
        {
            Debug.Log("No modifier in " + slotNumber);
            return null;
        }
    }
    public string GetModifierName()
    {
        if (modifier)
            return modifier.GetComponent<Effect>().modifier.modifierName;
        else
        {
            Debug.Log("No modifier in " + slotNumber);
            return null;
        }
    }

    private void Start()
    {
        equipmentUI = GameObject.FindWithTag("UI").GetComponentInChildren<EquipmentUI>();
        if (!equipmentUI)
        {
            Debug.LogError("No Equipment UI found for Equipment Slot!");
        }
    }
}
