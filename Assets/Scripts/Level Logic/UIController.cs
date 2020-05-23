using UnityEngine;
// using TMPro;

public class UIController : MonoBehaviour
{
    //Singleton Stuff
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

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

        DontDestroyOnLoad(this.gameObject);
    }

    // public TextMeshProUGUI healthText;

    public void setHealthText(int currentHealth, int maxHealth) {
        // healthText.setText(currentHealth + "/" + maxHealth);
    }
}
