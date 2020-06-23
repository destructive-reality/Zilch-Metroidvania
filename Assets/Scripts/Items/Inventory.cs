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
        equipment = gameObject.GetComponentInParent<Equipment>();
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(item.name + " added to Player Inventory");
        
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void UseItem(GameObject item, EquipmentSlot slot = EquipmentSlot.Body)
    {
        Debug.Log("Using " + item.name);
        if (item.GetComponent<Effect>())
        {
            equipment.EquipModifier(item, slot);
            items.Remove(item);
            
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
    }
    public void UseAll()
    {
        foreach (GameObject item in items)
        {
            UseItem(item);
            // Könnte einen Fehler werfen, da in UseItem ein item der Liste gelöscht werden könnte. What to do..? MD
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))    // For Testing  MD
        {
            UseAll();
        }
        if (Input.GetKeyDown(KeyCode.L) && items[0])    // For Testing  MD
        {
            UseItem(items[0], EquipmentSlot.Leg);
        }
        if (Input.GetKeyDown(KeyCode.O) && items[0])    // For Testing  MD
        {
            UseItem(items[0], EquipmentSlot.Body);
        }
        if (Input.GetKeyDown(KeyCode.P) && items[0])    // For Testing  MD
        {
            UseItem(items[0], EquipmentSlot.Arm);
        }
    }

    // if(modifier.effectBody.isStart)
    // {
    //     Invoke("Effects." + modifier.effectBody.effectName + "Start", 0);
    //     // modifier.effectBody.effectScript.Start();
    // }
    // protected OnEquipmentChange(EventArgs arguments) {

    // }

}
