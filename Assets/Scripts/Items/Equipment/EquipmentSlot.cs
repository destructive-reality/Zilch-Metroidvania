using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public Button ItemButton;
    public ModifierSlot slotPosition;
    private int slotNumber;
    private EquipmentUI equipmentUI;
    private GameObject modifier;

    public void InitializeEquipmentSlotPosition(int _i)
    {
        slotNumber = _i;
        switch (_i)
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
                slotPosition = ModifierSlot.Weapon;
                break;
        }
        icon.gameObject.GetComponent<DragDrop>().slotNumber = _i;
    }
    public void AddItem(GameObject _newModifier)
    {
        modifier = _newModifier;

        icon.sprite = modifier.GetComponent<Effect>().modifier.icon;
        icon.enabled = true;

        ItemButton.interactable = true;
    }

    public void ClearSlot()
    {
        modifier = null;

        icon.sprite = null;

        icon.enabled = false;

        ItemButton.interactable = false;
    }

    public void ChangeSlotOf(int _modifierSlotNumber)
    {
        // GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(modifier, slot);
        equipmentUI.StartSlotChange(_modifierSlotNumber, slotPosition);
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
