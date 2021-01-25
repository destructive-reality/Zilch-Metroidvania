using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public ModifierSlot currentSlot = ModifierSlot.None;
    public ModifierObject modifier;

    public SpriteRenderer pickupSpriteRenderer;

    public abstract void ArmStart(bool value = true);
    public abstract void LegStart(bool value = true);
    public abstract void WeaponStart(bool value = true);
    public abstract void HeadStart(bool value = true);
    public abstract void ArmUpdate();
    public abstract void LegUpdate();
    public abstract void WeaponUpdate();
    public abstract void HeadUpdate();

    private void Awake() {
        pickupSpriteRenderer.sprite = modifier.icon;
    }
}

public enum ModifierSlot { None, Head, Arm, Leg, Weapon }