using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject equipmentUI;
    private Equipment equipment;
    private EquipmentSlot[] slots;

    private Camera cam;

    public dynamic checkSlot(GameObject slot)
    {
        Debug.Log("Checking for Slot");
        Debug.Log(slot);
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(slots[i]);
            if (slot.name == slots[i].name)
            {
                Debug.Log("Slot is " + i);
                return i;
            }
        }
        return null;
    }
    public void StartSlotChange(int modifierSlotNumber, ModifierSlot slot){
        equipment.ChangeSlot(slots[modifierSlotNumber].GetModifier(), slot);
        slots[modifierSlotNumber].ClearSlot();
    }

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.allCameras[0];

        equipment = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();
        equipment.changedEquipmentCallback += UpdateEquipmentUI;

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].InitializeEquipmentSlotPosition(i);
            slots[i].icons[0].gameObject.GetComponent<DragDrop>().slotNumber = i;
        }
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
            slots[i].ClearSlot();
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < equipment.modifiers.Count)
            {
                Effect modifierEffect = equipment.modifiers[i].GetComponent<Effect>();
                if (modifierEffect.currentSlot == ModifierSlot.Head)
                {
                    if (slots[0].GetModifierName() == modifierEffect.modifier.modifierName || slots[0].GetModifierName() == null)
                    {
                        slots[0].AddItem(equipment.modifiers[i]);
                    }
                    else
                    {
                        Debug.Log("Head-slot is reserved. Can't equip " + equipment.modifiers[i].name);
                    }
                }
                else if (modifierEffect.currentSlot == ModifierSlot.Arm)
                {
                    if (slots[1].GetModifierName() == modifierEffect.modifier.modifierName || slots[1].GetModifierName() == null)
                    {
                        slots[1].AddItem(equipment.modifiers[i]);
                    }
                    else if (slots[2].GetModifierName() == modifierEffect.modifier.modifierName || slots[2].GetModifierName() == null)
                    {
                        slots[2].AddItem(equipment.modifiers[i]);
                    }
                    else
                    {
                        Debug.Log("Arm-slots are reserved. Can't equip " + equipment.modifiers[i].name);
                    }
                }
                else if (modifierEffect.currentSlot == ModifierSlot.Leg)
                {
                    if (slots[3].GetModifierName() == modifierEffect.modifier.modifierName || slots[3].GetModifierName() == null)
                    {
                        // Debug.Log("Slot 3 has " + slots[3].GetModifierName());
                        slots[3].AddItem(equipment.modifiers[i]);
                    }
                    else if (slots[4].GetModifierName() == modifierEffect.modifier.modifierName || slots[4].GetModifierName() == null)
                    {
                        // Debug.Log("Slot 4 has " + slots[4].GetModifierName());
                        slots[4].AddItem(equipment.modifiers[i]);
                    }
                    else
                    {
                        Debug.Log("Leg-slots are reserved. Can't equip " + equipment.modifiers[i].name);
                    }
                }
                else if (modifierEffect.currentSlot == ModifierSlot.Body)
                {
                    for (int j = 5; j < slots.Length; j++)
                    {
                        if (slots[j].GetModifierName() == modifierEffect.modifier.modifierName || slots[j].GetModifierName() == null)
                        {
                            slots[j].AddItem(equipment.modifiers[i]);
                            break;
                        }
                        else
                        {
                            Debug.Log("Body-slots are reserved. Can't equip " + modifierEffect.modifier.modifierName);
                        }
                    }
                }
                else
                {
                    Debug.Log("All slots are reserved!");
                }
            }
            else
            {
                // slots[i].ClearSlot();
            }
        }
    }
}
