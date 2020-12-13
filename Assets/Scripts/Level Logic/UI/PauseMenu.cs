using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    [Header("Submenu")]
    public InventoryUI inventoryUI;
    public EquipmentUI equipmentUI;

    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        //Replace with Input Key?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;

        equipmentUI.SetEquipmentUIVisible(false);
        inventoryUI.SetInventoryUIVisible(false);
    }

    public void LoadMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
