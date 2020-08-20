using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public delegate void OnEquipmentChange();
    public event OnEquipmentChange changedEquipmentCallback;
    public List<GameObject> modifiers;

    public bool ValidateOperation(ModifierSlot slot)
    {
        int counter = 0;
        int checker;
        switch (slot)
        {
            case ModifierSlot.Head:
                checker = 1;
                break;
            case ModifierSlot.Arm:
            case ModifierSlot.Leg:
                checker = 2;
                break;
            default:
                return true;
        }
        for (int i = 0; i < modifiers.Count; i++)
        {
            switch (slot)
            {
                case ModifierSlot.Head:
                    if (modifiers[i].GetComponent<Effect>().currentSlot == ModifierSlot.Head)
                    {
                        counter++;
                    }
                    break;
                case ModifierSlot.Arm:
                    if (modifiers[i].GetComponent<Effect>().currentSlot == ModifierSlot.Arm)
                    {
                        counter++;
                    }
                    break;
                case ModifierSlot.Leg:
                    if (modifiers[i].GetComponent<Effect>().currentSlot == ModifierSlot.Leg)
                    {
                        counter++;
                    }
                    break;
            }
            if (counter >= checker)
            {
                return false;
            }
        }
        return true;
    }
    public void EquipModifier(GameObject modifier, ModifierSlot slot)
    {
        modifiers.Add(modifier);
        Effect modifierEffect = modifier.GetComponent<Effect>();
        modifierEffect.currentSlot = slot;
        ExecuteStartEffect(modifierEffect, slot);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
    }
    private void ExecuteStartEffect(Effect modifierEffect, ModifierSlot slot, bool enable = true)
    {
        switch (slot)
        {
            case ModifierSlot.Arm:
                if (modifierEffect.modifier.effectArm.isStart)
                {
                    modifierEffect.ArmStart(enable);
                }
                break;
            case ModifierSlot.Leg:
                if (modifierEffect.modifier.effectLeg.isStart)
                {
                    modifierEffect.LegStart(enable);
                }
                break;
            case ModifierSlot.Body:
                if (modifierEffect.modifier.effectBody.isStart)
                {
                    modifierEffect.BodyStart(enable);
                }
                break;
            case ModifierSlot.Head:
                if (modifierEffect.modifier.effectHead.isStart)
                {
                    modifierEffect.HeadStart(enable);
                }
                break;
            default:
                Debug.LogWarning("Not a valid Equimpent-Slot");
                break;
        }
    }
    public void Unequip(GameObject modifier)
    {
        Effect modifierEffect = modifier.GetComponent<Effect>();
        ExecuteStartEffect(modifierEffect, modifierEffect.currentSlot, false);
        modifierEffect.currentSlot = ModifierSlot.None;
        modifiers.Remove(modifier);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
    }
    public void ChangeSlot(GameObject modifier, ModifierSlot slot)
    {
        Effect modifierEffect = modifier.GetComponent<Effect>();
        ModifierSlot oldSlot = modifierEffect.currentSlot;
        if (oldSlot == slot)
        {
            Debug.Log("Don't change modifier slot");
            return;
        }
        modifierEffect.currentSlot = slot;
        ExecuteStartEffect(modifierEffect, oldSlot, false);
        ExecuteStartEffect(modifierEffect, slot);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
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
                        default:       
                            Debug.LogWarning("Not a valid Equimpent-Slot");
                        break;
                }
            }
        }
    }



}
