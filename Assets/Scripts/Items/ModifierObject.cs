using UnityEngine;

[CreateAssetMenu(fileName = "New ModifierObject", menuName = "Assets/Modifier")]
public class ModifierObject : ScriptableObject
{
    public Sprite icon;
    public string modifierName = "New Modifier";
    public ModifierSlot currentSlot = ModifierSlot.None;
    public EffectObject effectBody = null;
    public EffectObject effectArm = null;
    public EffectObject effectLeg = null;
    public EffectObject effectHead = null;
    
    [TextArea(5, 15)] 
    public string modifierDescription = "Modifier description, also the place for some lore.";

}
