using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    private Inventory inventory;
    private InventorySlot[] slots;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        // inventory.onItemChangedCallback += UpdateInventoryUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].icon.gameObject.GetComponent<DragDrop>().slotNumber = i;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && !PauseMenu.GameIsPaused)
        {
            SetInventoryUIVisible(!inventoryUI.activeSelf);
        }
    }

    public void AddNewItem(GameObject _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }

    }

    public void SetInventoryUIVisible(bool visibleBoolean) {
         inventoryUI.SetActive(visibleBoolean);
    }
}