using UnityEngine;

[CreateAssetMenu(fileName = "New EffectObject", menuName = "Assets/Effect Object")]
public class EffectObject : ScriptableObject
{
  public string effectName;
  public bool isUpdate;
  public bool isStart;
  public string effectDiscription;
}