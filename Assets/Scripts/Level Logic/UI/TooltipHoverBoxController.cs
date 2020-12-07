using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipHoverBoxController : MonoBehaviour
{

    //Singleton Stuff
    private static TooltipHoverBoxController _instance;
    public static TooltipHoverBoxController Instance { get { return _instance; } }

    // Vars
    public int pixelOffsetDistance;

    public GameObject tooltipContentParent;
    public RectTransform tooltipContentParentRectTransform;
    public Text toolTipText;

    private int halfScreenWidth;
    private int halfScreenHeight;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;

        tooltipContentParentRectTransform = tooltipContentParent.GetComponent<RectTransform>();
    }
    void Update()
    {
        if (tooltipContentParent.activeSelf)
        {
            Vector2 newMousePosition = Input.mousePosition;
            Vector2 newTransformPosition = transform.position;

            newTransformPosition = newMousePosition;

            newTransformPosition.x += newMousePosition.x < halfScreenWidth ? pixelOffsetDistance + tooltipContentParentRectTransform.rect.width : -pixelOffsetDistance;
            newTransformPosition.y += newMousePosition.y > halfScreenHeight ? pixelOffsetDistance - tooltipContentParentRectTransform.rect.height : -pixelOffsetDistance;

            transform.position = newTransformPosition;
        }
    }

    public void setVisible(bool shouldBeVisible)
    {
        tooltipContentParent.SetActive(shouldBeVisible);
    }

    public void changeText(string newText)
    {
        toolTipText.text = newText;
    }
}
