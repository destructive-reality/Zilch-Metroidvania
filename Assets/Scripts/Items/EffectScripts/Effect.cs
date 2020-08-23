using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public ModifierSlot currentSlot = ModifierSlot.None;
    public ModifierObject modifier;

    public abstract void ArmStart(bool value = true);
    public abstract void LegStart(bool value = true);
    public abstract void BodyStart(bool value = true);
    public abstract void HeadStart(bool value = true);
    public abstract void ArmUpdate();
    public abstract void LegUpdate();
    public abstract void BodyUpdate();
    public abstract void HeadUpdate();
}

public enum ModifierSlot { None, Arm, Leg, Body, Head }