using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onItemChangedCallback;

    public List<GameObject> items;
    private Equipment equipment;

    private void Awake()
    {
        Debug.Log("Try getting Equipment for Inventory");
        equipment = gameObject.GetComponentInParent<Equipment>();
        Debug.Log("Get Equipment for Inventory: " + equipment.gameObject.name);
    }

    public void AddItem(GameObject _item)
    {
        items.Add(_item);
        Debug.Log(_item.name + " added to Player Inventory");
        
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void UseItem(GameObject _item, ModifierSlot _slot = ModifierSlot.Weapon)
    {
        Debug.Log("Using " + _item.name + " on " + _slot.ToString());
        if (_item.GetComponent<Effect>() && equipment.ValidateOperation(_slot))
        {
            equipment.EquipModifier(_item, _slot);
            items.Remove(_item);
            
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }
    // public void UseAll()
    // {
    //     foreach (GameObject item in items)
    //     {
    //         UseItem(item);
    //         // Könnte einen Fehler werfen, da in UseItem ein item der Liste gelöscht werden könnte. What to do..? MD
    //     }
    // }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.L) && items[0])    // For Testing  MD
    //     {
    //         UseItem(items[0], ModifierSlot.Leg);
    //     }
    //     if (Input.GetKeyDown(KeyCode.O) && items[0])    // For Testing  MD
    //     {
    //         UseItem(items[0], ModifierSlot.Body);
    //     }
    //     if (Input.GetKeyDown(KeyCode.P) && items[0])    // For Testing  MD
    //     {
    //         UseItem(items[0], ModifierSlot.Arm);
    //     }
    // }
}
