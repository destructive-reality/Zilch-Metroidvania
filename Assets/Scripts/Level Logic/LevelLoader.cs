using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Singleton Stuff
    private static LevelLoader _instance;
    public static LevelLoader Instance { get { return _instance; } }

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
    }

    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        LoadLevelWithID(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReloadCurrentLevelAfterSeconds(float seconds)
    {
        Invoke("ReloadCurrentLevel", seconds);
    }

    public void LoadLevelWithID(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
