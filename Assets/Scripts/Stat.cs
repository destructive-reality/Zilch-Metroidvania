using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField]
    private float baseValue;
    private List<float> modifiers = new List<float>();

    public float getValue() {
        float value = baseValue;
        modifiers.ForEach(modifier => value += modifier);
        return value;
    }
    public void addModifier (float modifier) {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void removeModifier (float modifier) {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }


}
