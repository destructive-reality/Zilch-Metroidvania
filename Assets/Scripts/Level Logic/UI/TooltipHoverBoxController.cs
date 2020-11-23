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
    public Vector2 offset;

    public GameObject tooltipContentParent;
    public Text toolTipText;

     private void Awake () {
        if (_instance != null && _instance != this) {
            Destroy (this.gameObject);
        } else {
            _instance = this;
        }
    }
    void Update()
    {
        transform.position = Input.mousePosition + (Vector3)offset;
    }

    public void setVisible(bool shouldBeVisible) {
        tooltipContentParent.SetActive(shouldBeVisible);
    }

    public void changeText(string newText) {
        toolTipText.text = newText;
    }
}
