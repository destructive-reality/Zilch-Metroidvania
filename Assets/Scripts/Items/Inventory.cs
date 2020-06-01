using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ModifierObject> modifiers;
    
    public void AddModifier(ModifierObject modifier) {
        modifiers.Add(modifier);
        Debug.Log(modifier.modifierName + " added to Player Inventory");
    }
}
