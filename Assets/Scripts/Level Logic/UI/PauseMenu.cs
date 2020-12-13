using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

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
