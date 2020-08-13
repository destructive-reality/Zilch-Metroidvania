using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public delegate void OnEquipmentChange();
    public event OnEquipmentChange changedEquipmentCallback;
    public List<GameObject> modifiers;

    public void EquipModifier(GameObject modifier, ModifierSlot slot)
    {
        modifiers.Add(modifier);
        Effect modifierEffect = modifier.GetComponent<Effect>();
        modifierEffect.currentSlot = slot;
        switch (slot)
        {
            case ModifierSlot.Arm:
                if (modifierEffect.modifier.effectArm.isStart)
                {
                    modifierEffect.ArmStart();
                }
                break;
            case ModifierSlot.Leg:
                if (modifierEffect.modifier.effectLeg.isStart)
                {
                    modifierEffect.LegStart();
                }
                break;
            case ModifierSlot.Body:
                if (modifierEffect.modifier.effectBody.isStart)
                {
                    modifierEffect.BodyStart();
                }
                break;
            case ModifierSlot.Head:
                if (modifierEffect.modifier.effectHead.isStart)
                {
                modifierEffect.HeadStart();
                }
                break;
            // default:        // throws Error MD
                //     Debug.LogWarning("Not a valid Equimpent-Slot");
        }
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
    }
    public void Unequip() {

    }
    public void ChangeSlot(ModifierSlot slot) {

    }
    private void Update()
    {
        if (modifiers.Count > 0)
        {
            foreach (GameObject modifier in modifiers)
            {
                Effect modifierEffect = modifier.GetComponent<Effect>();
                switch (modifierEffect.currentSlot)
                {
                    case ModifierSlot.Arm:
                        if (modifierEffect.modifier.effectArm.isUpdate)
                        {
                            modifierEffect.ArmUpdate();
                        }
                        break;
                    case ModifierSlot.Leg:
                        if (modifierEffect.modifier.effectLeg.isUpdate)
                        {
                            modifierEffect.LegUpdate();
                        }
                        break;
                    case ModifierSlot.Body:
                        if (modifierEffect.modifier.effectBody.isUpdate)
                        {
                            modifierEffect.BodyUpdate();
                        }
                        break;
                    case ModifierSlot.Head:
                        if (modifierEffect.modifier.effectHead.isUpdate)
                        {
                            modifierEffect.HeadUpdate();
                        }
                        break;
                    // default:        // throws Error MD
                        //     Debug.LogWarning("Not a valid Equimpent-Slot");
                }
            }
        }
    }



}
