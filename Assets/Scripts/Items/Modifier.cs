using UnityEngine;

public class Modifier : MonoBehaviour
{
    [SerializeField] private GameObject modifierEffect;
    private SpriteRenderer pickUpSprite;
    private bool isInteractable = false;
    private Inventory playerInventory;

    private void Awake() {
        pickUpSprite = GetComponentInChildren<SpriteRenderer>();
        pickUpSprite.enabled = isInteractable;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = pickUpSprite.enabled = true;
            playerInventory = other.GetComponentInChildren<Inventory>();
        }
    }
    private void Update()
    {
        if (isInteractable && Input.GetButtonDown("PickUp") && playerInventory)
        {
            playerInventory.AddItem(modifierEffect);
            // modifierEffect.GetComponent<Effects>().currentSlot = EquipmentSlot.Body;
            modifierEffect.transform.parent = playerInventory.gameObject.transform;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = pickUpSprite.enabled = false;
        }
    }
}
