using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject equipmentUI;
    private Equipment equipment;
    private EquipmentSlot[] slots;

    Camera cam;

    public dynamic checkSlot(GameObject slot){
        Debug.Log("Checking for Slot");
        Debug.Log(slot);
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(slots[i]);
            if (slot.name == slots[i].name)
            {
                Debug.Log("Slot is "+ i);
                return i;
            }
        }
        return null;
    }

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.allCameras[0];

        equipment = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();
        equipment.changedEquipmentCallback += UpdateEquipmentUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))    // Sollte noch zu einem Unity Button-Teil werden statt der feste Keycode
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Debug.Log("Cast Ray from " + Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name + " was hit.");
            }
            // else 
                // Debug.Log("No hit :(");
        }
    }

    private void UpdateEquipmentUI()
    {
        Debug.Log("Updating Equipment UI");
        for (int i = 0; i < slots.Length; i++)
        { 
            if (i < equipment.modifiers.Count)
            {
                slots[i].AddItem(equipment.modifiers[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
