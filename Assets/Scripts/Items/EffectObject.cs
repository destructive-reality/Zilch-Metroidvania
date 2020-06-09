using UnityEngine;

[CreateAssetMenu(fileName = "New EffectObject", menuName = "Assets/Modifier Effect")]
public class EffectObject : ScriptableObject
{
    public string effectName;
    // public MonoBehaviour effectScript;
    public bool isUpdate;
    public bool isStart;
}
