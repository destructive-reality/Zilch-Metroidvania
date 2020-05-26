using UnityEngine;
using UnityEngine.UI;

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

    [Header("UI References")]
    public Text healthText;
    public Slider healthBar;

    public void setHealthInUI(int newCurrentHealth, int maxHealth) {
        healthText.text = newCurrentHealth + "/" + maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = newCurrentHealth;
    }
}
