using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image[] icons;
    public Button ArmButton;
    public Button LegButton;
    public Button BodyButton;
    GameObject modifier;

    public void AddItem(GameObject newModifier)
    {
        modifier = newModifier;

        icons[0].sprite = modifier.GetComponent<Effect>().modifier.icon;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = true;
        }
        ArmButton.interactable = BodyButton.interactable = LegButton.interactable = true;
    }

    public void ClearSlot()
    {
        modifier = null;

        icons[0].sprite = null;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].enabled = false;
        }
        ArmButton.interactable = BodyButton.interactable = LegButton.interactable = false;
    }

    public void OnArmButton()
    {
        Debug.Log("ArmButton clicked");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, EquipmentSlot.Arm);
    }
    public void OnLegButton()
    {
        Debug.Log("LegButton clicked");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, EquipmentSlot.Leg);
    }
    public void OnBodyButton()
    {
        Debug.Log("BodyButton clicked");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipModifier(modifier, EquipmentSlot.Body);
    }
}
