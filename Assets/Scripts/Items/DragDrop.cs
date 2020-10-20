using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Item-Info
    public int slotNumber;
    public bool isEquipment;
    #endregion
    
    #region UI-Info
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    #endregion

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvas = GameObject.FindWithTag("UI").GetComponent<Canvas>();
        if (!canvas)
        {
            Debug.LogError("No Canvas found!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Item Text anzeigen?
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        if (Input.GetButtonDown("Inventory") || Input.GetButtonDown("Equipment"))
        {
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Release Drag");
        if (eventData.pointerEnter)
        {
            if (eventData.pointerEnter.GetComponentInParent<InventorySlot>())
            {
                // InventorySlot targetScript = eventData.pointerEnter.GetComponentInParent<InventorySlot>();
                if (isEquipment)
                {
                    Debug.Log("Unequip the item at slot " + slotNumber);
                    EquipmentSlot slotScript = GetComponentInParent<EquipmentSlot>();
                    Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
                    Equipment equipment = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Equipment>();

                    inventory.AddItem(slotScript.GetModifier());
                    equipment.UnequipModifier(slotScript.GetModifier());
                    // Inventory.AddItem
                    // Equipment.UnequipModifier(Modifier)

                }
            }
            else if (eventData.pointerEnter.GetComponentInParent<EquipmentSlot>())
            {
                EquipmentSlot targetScript = eventData.pointerEnter.GetComponentInParent<EquipmentSlot>();
                if (isEquipment)
                {
                    targetScript.ChangeSlotOf(slotNumber);
                }
                else
                {
                    Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Inventory>();
                    Debug.Log("Slot number: " + slotNumber);
                    Debug.Log("Equip to position: " + targetScript.slotPosition);
                    inventory.UseItem(inventory.items[slotNumber], targetScript.slotPosition);
                }
            }
        }
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}