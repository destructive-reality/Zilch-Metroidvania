using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public ModifierSlot currentSlot = ModifierSlot.None;
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

public enum ModifierSlot { None, Arm, Leg, Body, Head }