using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public delegate void OnEquipmentChange();
    public event OnEquipmentChange changedEquipmentCallback;
    public List<GameObject> modifiers;

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
                    case ModifierSlot.Weapon:
                        if (modifierEffect.modifier.effectBody.isUpdate)
                        {
                            modifierEffect.WeaponUpdate();
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
    
    public bool ValidateOperation(ModifierSlot _slot)
    {
        int counter = 0;
        int checker;
        switch (_slot)
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
            switch (_slot)
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
    
    public void EquipModifier(GameObject _modifier, ModifierSlot _slot)
    {
        modifiers.Add(_modifier);
        Effect modifierEffect = _modifier.GetComponent<Effect>();
        modifierEffect.currentSlot = _slot;
        ExecuteStartEffect(modifierEffect, _slot);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
    }

    public void EquipModifier(GameObject _modifierToEquip, EquipmentSlot _slotScript)
    {
        modifiers.Add(_modifierToEquip);
        Effect modifierEffect = _modifierToEquip.GetComponent<Effect>();
        modifierEffect.currentSlot = _slotScript.slotPosition;
        ExecuteStartEffect(modifierEffect, modifierEffect.currentSlot);
    }

    public void UnequipModifier(GameObject _modifier)
    {
        Effect modifierEffect = _modifier.GetComponent<Effect>();
        ExecuteStartEffect(modifierEffect, modifierEffect.currentSlot, false);
        modifierEffect.currentSlot = ModifierSlot.None;
        modifiers.Remove(_modifier);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
    }
    
    public bool ChangeSlot(GameObject _modifier, ModifierSlot _slot)
    {
        Effect modifierEffect = _modifier.GetComponent<Effect>();
        ModifierSlot oldSlot = modifierEffect.currentSlot;
        if (oldSlot == _slot)
        {
            Debug.Log("Don't change modifier slot");
            return false;
        }
        modifierEffect.currentSlot = _slot;
        ExecuteStartEffect(modifierEffect, oldSlot, false);
        ExecuteStartEffect(modifierEffect, _slot);
        if (changedEquipmentCallback != null)
        {
            changedEquipmentCallback.Invoke();
        }
        return true;
    }
    
    private void ExecuteStartEffect(Effect _modifierEffect, ModifierSlot _slot, bool _isToBeEnabled = true)
    {
        switch (_slot)
        {
            case ModifierSlot.Arm:
                if (_modifierEffect.modifier.effectArm.isStart)
                {
                    _modifierEffect.ArmStart(_isToBeEnabled);
                }
                break;
            case ModifierSlot.Leg:
                if (_modifierEffect.modifier.effectLeg.isStart)
                {
                    _modifierEffect.LegStart(_isToBeEnabled);
                }
                break;
            case ModifierSlot.Weapon:
                if (_modifierEffect.modifier.effectBody.isStart)
                {
                    _modifierEffect.WeaponStart(_isToBeEnabled);
                }
                break;
            case ModifierSlot.Head:
                if (_modifierEffect.modifier.effectHead.isStart)
                {
                    _modifierEffect.HeadStart(_isToBeEnabled);
                }
                break;
            default:
                Debug.LogWarning("Not a valid Equimpent-Slot");
                break;
        }
    }
}