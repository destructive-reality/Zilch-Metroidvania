using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Assets/Modifier Effect")]
public class EffectObject : ScriptableObject
{
    public string effectName;
    // public MonoBehaviour effectScript;
    public bool isUpdate;
    public bool isStart;
}
