using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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

  [Header("Sounds")]
  public AudioClip itemHoverSound;

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

  //   public void OnPointerDown(PointerEventData eventData)
  //   {
  //     // Item Text anzeigen?
  //   }

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
    // Debug.Log("Release Drag");
    if (eventData.pointerEnter)
    {
      Slot slotScript = GetComponentInParent<Slot>();
      Slot targetScript = eventData.pointerEnter.GetComponentInParent<Slot>();
      Debug.Log(targetScript.gameObject.name);
      targetScript.MoveItem(slotScript.GetItem(), slotScript);

    }
    rectTransform.anchoredPosition = new Vector2(0, 0);
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    TooltipHoverBoxController.Instance.setVisible(true);

    // ModifierObject hoveredModifier = GetComponentInParent<Slot>().GetItem().GetComponent<Effect>().modifier;
    Effect hoveredModifierEffect = GetComponentInParent<Slot>().GetItem().GetComponent<Effect>();
    switch (hoveredModifierEffect.currentSlot)
    {
      case ModifierSlot.Head:
        TooltipHoverBoxController.Instance.changeText($"{hoveredModifierEffect.modifier.modifierName}\n\n{hoveredModifierEffect.modifier.effectHead.effectDiscription}");
        break;
      case ModifierSlot.None:
        TooltipHoverBoxController.Instance.changeText($"{hoveredModifierEffect.modifier.modifierName}\n\n{hoveredModifierEffect.modifier.modifierDescription}");
        break;
      case ModifierSlot.Arm:
        TooltipHoverBoxController.Instance.changeText($"{hoveredModifierEffect.modifier.modifierName}\n\n{hoveredModifierEffect.modifier.effectArm.effectDiscription}");
        break;
      case ModifierSlot.Leg:
        TooltipHoverBoxController.Instance.changeText($"{hoveredModifierEffect.modifier.modifierName}\n\n{hoveredModifierEffect.modifier.effectLeg.effectDiscription}");
        break;
      case ModifierSlot.Weapon:
        TooltipHoverBoxController.Instance.changeText($"{hoveredModifierEffect.modifier.modifierName}\n\n{hoveredModifierEffect.modifier.effectWeapon.effectDiscription}");
        break;
    }



    //Play sound on hover
    UIController.Instance.getUiAudioSource().PlayOneShot(itemHoverSound);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    TooltipHoverBoxController.Instance.setVisible(false);
  }
}