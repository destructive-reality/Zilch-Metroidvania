using UnityEngine;

public class EquipmentSlot : Slot
{
  public ModifierSlot slotPosition;
  private Equipment equipment;
  private int slotNumber;

  private void Start()
  {
    equipment = GameObject.FindWithTag("Player").GetComponent<Equipment>();
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

  public override void AddItem(GameObject _newModifier)
  {
    base.AddItem(_newModifier);
    equipment.EquipModifier(_newModifier, this);
  }

  public override void ClearSlot()
  {
    // Debug.Log("is item null?" + item);
    if (item != null)
      equipment.UnequipModifier(item);

    base.ClearSlot();
  }
}