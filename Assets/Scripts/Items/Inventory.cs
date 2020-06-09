using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;
    private Equipment equipment;

    private void Awake() {
        equipment = gameObject.GetComponentInParent<Equipment>();
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(item.name + " added to Player Inventory");
    }
    public void UseItem(GameObject item)
    {
        Debug.Log("Using " + item.name);
        if (item.GetComponent<Effect>())
        {
            equipment.EquipModifier(item, EquipmentSlot.Body);
            items.Remove(item);
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
    }

    // if(modifier.effectBody.isStart)
    // {
    //     Invoke("Effects." + modifier.effectBody.effectName + "Start", 0);
    //     // modifier.effectBody.effectScript.Start();
    // }
    // protected OnEquipmentChange(EventArgs arguments) {

    // }

}
