using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image[] icons;
    GameObject modifier;

    public void AddItem(GameObject newModifier)
    {
        modifier = newModifier;

        icons[0].sprite = modifier.GetComponent<Effect>().modifier.icon;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = true;
        }
    }

    public void ClearSlot()
    {
        modifier = null;

        icons[0].sprite = null;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = false;
        }
    }

    public void OnArmButton()
    {
        Debug.Log("ArmButton clicked");
        EquipOnSlot(ModifierSlot.Arm);
    }
    public void OnLegButton()
    {
        Debug.Log("LegButton clicked");
        EquipOnSlot(ModifierSlot.Leg);
    }
    public void OnBodyButton()
    {
        Debug.Log("BodyButton clicked");
        EquipOnSlot(ModifierSlot.Body);
    }
    public void EquipOnSlot(ModifierSlot slot) {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, slot);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>().UseItem(modifier, slot);
    }
}
