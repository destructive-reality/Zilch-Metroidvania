using UnityEngine;

public class EquipmentSlot : Slot
{
    public ModifierSlot slotPosition;
    private int slotNumber;
    private EquipmentUI equipmentUI;

    private void Start()
    {
        equipmentUI = GameObject.FindWithTag("UI").GetComponentInChildren<EquipmentUI>();
        if (!equipmentUI)
        {
            Debug.LogError("No Equipment UI found for Equipment Slot!");
        }
    }

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

    public void ChangeSlotOf(int _modifierSlotNumber)
    {
        equipmentUI.StartSlotChange(_modifierSlotNumber, slotPosition);
    }
    
    public string GetModifierName()
    {
        if (item)
            return item.GetComponent<Effect>().modifier.modifierName;
        else
        {
            Debug.Log("No modifier in " + slotNumber);
            return null;
        }
    }
}