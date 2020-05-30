using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Assets/Modifier")]
public class ModifierObject : ScriptableObject
{
    public string modifierName = "New Modifier";
    public EffectObject effectBody = null;
    public EffectObject effectArm = null;
    public EffectObject effectLeg = null;
    
    [TextArea(5, 15)] 
    public string modifierDescription = "Modifier description, also the place for some lore.";

}
