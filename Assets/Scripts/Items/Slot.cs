using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public Image icon;
    public Button itemButton;
    protected GameObject item;

    public void AddItem(GameObject _newModifier)
    {
        item = _newModifier;
        icon.sprite = item.GetComponent<Effect>().modifier.icon;
        EnableInteraction(true);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        EnableInteraction(false);
    }

    public GameObject GetItem()
    {
        if (item)
            return item;
        else
            return null;
    }

    public void MoveItem(GameObject _newItem, Slot _fromSlot)
    {
        if (item)
            _fromSlot.AddItem(item);
        else
            _fromSlot.ClearSlot();

        AddItem(_newItem);
    }

    protected void EnableInteraction(bool _value)
    {
        icon.enabled = _value;
        itemButton.interactable = _value;
    }

}