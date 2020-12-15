using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;
    private GameObject equipmentUIGO;
    private Equipment equipment;
    private EquipmentSlot[] slots;

    private void Start()
    {
        equipmentUIGO = itemsParent.gameObject;
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();
        /* equipment.changedEquipmentCallback += UpdateEquipmentUI; */

        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].InitializeEquipmentSlotPosition(i);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Equipment") && !PauseMenu.GameIsPaused)
        {
            SetEquipmentUIVisible(!equipmentUIGO.activeSelf);
        }
    }

    public void SetEquipmentUIVisible(bool visibleBoolean) {
         equipmentUIGO.SetActive(visibleBoolean);
    }
}