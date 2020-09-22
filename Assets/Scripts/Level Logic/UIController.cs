using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    //Singleton Stuff
    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

    [Header ("UI References")]
    public Text healthText;
    public Slider healthBar;
    public GameObject helth;
    private List<Image> helthImages;

    private void Awake () {
        if (_instance != null && _instance != this) {
            Destroy (this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad (this.gameObject);
        helthImages = new List<Image> (helth.GetComponentsInChildren<Image> ());
    }

    public void setHealthInUI (int newCurrentHealth, int maxHealth) {
        healthText.text = newCurrentHealth + "/" + maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = newCurrentHealth;

        if (maxHealth > helthImages.Count) {
            addMaxHealth (maxHealth - helthImages.Count);
        } else if (maxHealth < helthImages.Count) {
            reduceMaxHealth (helthImages.Count - maxHealth);
        }

        for (int i = 0; i < maxHealth; i++) {
            if (i < newCurrentHealth) {
                helthImages[i].color = new Vector4 (1, 1, 1, 1);
            } else {
                helthImages[i].color = new Vector4 (0.5f, 0.5f, 0.5f, 0.3f);
            }
        }
    }

    private void addMaxHealth (int amount) {
        Debug.LogWarning ("Max Health bigger than Images by " + amount);
        for (int i = 0; i < amount; i++) {
            Debug.Log ("Add " + amount + " HelthBlobs");
            GameObject newHelth = new GameObject ("HelthBlob (" + helthImages.Count + ")", typeof (CanvasRenderer), typeof (Image));
            newHelth.transform.SetParent (helth.transform);
            newHelth.transform.localScale = new Vector3 (1, 1, 1);

            Image newHelthImage = newHelth.GetComponent<Image> ();
            newHelthImage.sprite = helthImages[0].sprite;
            helthImages.Add (newHelthImage);
        }
    }

    private void reduceMaxHealth (int amount) {
        for (int i = 0; i < amount; i++) {
            Image objectToDelete = helthImages[helthImages.Count - 1];
            helthImages.Remove (objectToDelete);
            GameObject.Destroy (objectToDelete.gameObject);
        }
    }
}