using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class LoadSceneOnVideoFinish : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public int levelToLoad;

    void Start()
    {

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        LevelLoader.Instance.LoadLevelWithID(levelToLoad);
    }
}
