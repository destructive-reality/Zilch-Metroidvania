using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
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

    protected void EnableInteraction(bool _value)
    {
        icon.enabled = _value;
        itemButton.interactable = _value;
    }

}
