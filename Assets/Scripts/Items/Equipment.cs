using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public delegate void OnEquipmentChange();
    public event OnEquipmentChange changedEquipment;
    public List<GameObject> modifiers;

    public void EquipModifier(GameObject modifier, EquipmentSlot slot)
    {
        modifiers.Add(modifier);
        Effect modifierEffect = modifier.GetComponent<Effect>();
        modifierEffect.currentSlot = slot;
        switch (slot)
        {
            case EquipmentSlot.Arm:
                if (modifierEffect.modifier.effectArm.isStart)
                {
                    modifierEffect.ArmStart();
                }
                break;
            case EquipmentSlot.Leg:
                if (modifierEffect.modifier.effectLeg.isStart)
                {
                    modifierEffect.LegStart();
                }
                break;
            case EquipmentSlot.Body:
                if (modifierEffect.modifier.effectBody.isStart)
                {
                    modifierEffect.BodyStart();
                }
                break;
            case EquipmentSlot.Head:
                // if (modifierEffect.modifier.effectHead.isStart)
                // {
                // modifierEffect.HeadStart();
                // }
                break;
            // default:        // throws Error MD
                //     Debug.LogWarning("Not a valid Equimpent-Slot");
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
                    case EquipmentSlot.Arm:
                        if (modifierEffect.modifier.effectArm.isUpdate)
                        {
                            modifierEffect.ArmUpdate();
                        }
                        break;
                    case EquipmentSlot.Leg:
                        if (modifierEffect.modifier.effectLeg.isUpdate)
                        {
                            modifierEffect.LegUpdate();
                        }
                        break;
                    case EquipmentSlot.Body:
                        if (modifierEffect.modifier.effectBody.isUpdate)
                        {
                            modifierEffect.BodyUpdate();
                        }
                        break;
                    case EquipmentSlot.Head:
                        // if (modifierEffect.modifier.effectHead.isUpdate)
                        // {
                            // modifierEffect.HeadUpdate();
                        // }
                        break;
                    // default:        // throws Error MD
                        //     Debug.LogWarning("Not a valid Equimpent-Slot");
                }
            }
        }
    }
}
