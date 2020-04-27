﻿using System;
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

    void Update()
    {
        //TODO
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        LoadLevelWithID(SceneManager.GetActiveScene().buildIndex + 1);
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
}