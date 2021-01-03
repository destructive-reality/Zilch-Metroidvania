using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class LoadSceneOnVideoFinish : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int levelToLoad;

    void Start()
    {
        if (!videoPlayer)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        else
        {
            Debug.LogError("No VideoPlayer found");
        }
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
