using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    private Inventory inventory;
    private InventorySlot[] slots;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.allCameras[0];

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
        inventory.onItemChangedCallback += UpdateInventoryUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNumber = i;
            slots[i].icons[0].gameObject.GetComponent<DragDrop>().slotNumber = i;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory")) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetMouseButtonDown(0))    // auch Unity-Button für Linksklick? Sollte sich ja nicht ändern denke; außer viell. zu nem Rechtsklick für Linkshänder
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Debug.Log("Cast Ray from " + Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name + " was hit.");
            }
        }
    }

    void UpdateInventoryUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

}
