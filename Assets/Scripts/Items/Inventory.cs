using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ModifierObject> modifiers;

    private void Update()
    {
        if (modifiers.Count > 0)
        {
            foreach (ModifierObject modifier in modifiers)
            {
                if (modifier.effectBody.isUpdate)
                {
                    // modifier.effectBody.effectScript.Update();
                }
            }
        }
    }
    public void AddModifier(ModifierObject modifier)
    {
        modifiers.Add(modifier);
        Debug.Log(modifier.modifierName + " added to Player Inventory");
        if(modifier.effectBody.isStart)
        {
            Invoke("Effects." + modifier.effectBody.effectName + "Start", 0);
            // modifier.effectBody.effectScript.Start();
        }
    }
}
