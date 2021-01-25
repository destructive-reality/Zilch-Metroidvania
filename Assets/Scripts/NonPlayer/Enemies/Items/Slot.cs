using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    public Image icon;
    public Button itemButton;
    protected GameObject item;

    [Header("Sounds")]
    public AudioClip itemAddSound;

    public virtual void AddItem(GameObject _newModifier)
    {
        item = _newModifier;
        icon.sprite = item.GetComponent<Effect>().modifier.icon;
        EnableInteraction(true);
        if (InventoryUI.isInventoryVisible)
        {
            UIController.Instance.getUiAudioSource().PlayOneShot(itemAddSound);

        }
    }

    public virtual void ClearSlot()
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
        {
            GameObject tempItem = item;
            _fromSlot.ClearSlot();
            ClearSlot();
            _fromSlot.AddItem(tempItem);
        }
        else
        {
            // Debug.Log("Clear slot with Name: " + _fromSlot.gameObject.name);
            _fromSlot.ClearSlot();
        }
        AddItem(_newItem);
    }

    protected void EnableInteraction(bool _value)
    {
        icon.enabled = _value;
        itemButton.interactable = _value;
    }

}