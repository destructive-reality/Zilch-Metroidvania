using UnityEngine;

[CreateAssetMenu(fileName = "New EffectsScript", menuName = "Assets/Modifier")]
public abstract class Effect : MonoBehaviour
{
    public EquipmentSlot currentSlot = EquipmentSlot.None;
    public ModifierObject modifier;

    public abstract void ArmStart();
    public abstract void LegStart();
    public abstract void BodyStart();
    public abstract void HeadStart();
    public abstract void ArmUpdate();
    public abstract void LegUpdate();
    public abstract void BodyUpdate();
    public abstract void HeadUpdate();
}

public enum EquipmentSlot { None, Arm, Leg, Body, Head }