using UnityEngine;
using UnityEngine.Rendering;

public class PostProController : MonoBehaviour
{
    #region Singleton
    //Singleton Stuff
    private static PostProController _instance;
    public static PostProController Instance { get { return _instance; } }

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

    #endregion

    public Volume postProcessingVolume;
    public Animator animator;

    [Header("PostPro Values")]
    [Range(0.0f, 1.0f)]
    public float chromaticAberrationIntensity;

    private UnityEngine.Rendering.Universal.ChromaticAberration chromaticAberration;

    private void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        if (!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));

    }


    // Update is called once per frame
    void Update()
    {
        //Only update when changed
        if (chromaticAberration.intensity.value != chromaticAberrationIntensity)
        {
            chromaticAberration.intensity.Override(chromaticAberrationIntensity);

        }
    }

    public void TriggerChromaticAberrationDamageAnimation() {
        Debug.Log("Triggering CA");
        animator.SetTrigger("ChromaticAberrationDamage");
    }
}
