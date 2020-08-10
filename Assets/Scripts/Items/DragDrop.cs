using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start() {
        canvas = GameObject.FindWithTag("UI").GetComponent<Canvas>();
        if (!canvas)
        {
            Debug.LogError("No Canvas found!");
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Release Drag");
        if (rectTransform.anchoredPosition != new Vector2(0, 0))
        {
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}
