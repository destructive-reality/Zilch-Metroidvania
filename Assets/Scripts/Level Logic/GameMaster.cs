using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    //Singleton Stuff
    private static GameMaster _instance;
    public static GameMaster Instance { get { return _instance; } }

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

    public int lastLevelDoorId;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        //Set Player position next to Door
        GameObject[] currentLevelConnectors = GameObject.FindGameObjectsWithTag("LevelConnector");

        foreach (GameObject levelConnector in currentLevelConnectors)
        {
            LevelConnector levelConnectorScript = levelConnector.GetComponent<LevelConnector>();
            if (levelConnectorScript.doorId == lastLevelDoorId)
            {
                Debug.Log(levelConnector);
                GameObject.FindGameObjectWithTag("Player").transform.position = (Vector2)levelConnector.transform.position + LevelConnector.SpawnOffset(levelConnectorScript.enterDirection);
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
