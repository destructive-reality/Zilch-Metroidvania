using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    //Singleton Stuff
    private static GameMaster _instance;
    public static GameMaster Instance { get { return _instance; } }
    public int lastLevelDoorId;

    #region Slowdown
    private float slowdownTimer;
    private float slowdownTimerTotal;
    private float slowdownTargetScale;

    #endregion Slowdown

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

    private void Update()
    {
        if (slowdownTimer > 0)
        {
            slowdownTimer -= Time.unscaledDeltaTime;

            //Timer done
            Time.timeScale = Mathf.Lerp(slowdownTargetScale, 1f, (1 - (slowdownTimer / slowdownTimerTotal)));
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    public void SlowdownTime(float slowdownTargetScale, float time)
    {
        this.slowdownTargetScale = slowdownTargetScale;
        slowdownTimerTotal = time;
        slowdownTimer = time;
    }
}
