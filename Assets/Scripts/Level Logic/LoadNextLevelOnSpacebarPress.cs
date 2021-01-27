using UnityEngine;

public class LoadNextLevelOnSpacebarPress : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelLoader.Instance.LoadNextLevel();
        }
    }
}
