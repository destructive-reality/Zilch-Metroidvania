using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Button ItemButton;
    protected GameObject item;

    public void AddItem(GameObject newModifier)
    {
        item = newModifier;
        icon.sprite = item.GetComponent<Effect>().modifier.icon;
        EnableInteraction(true);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        EnableInteraction(false);
    }

    protected void EnableInteraction(bool value)
    {
        icon.enabled = value;
        ItemButton.interactable = value;
    }

}
