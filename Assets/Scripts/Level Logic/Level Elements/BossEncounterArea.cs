using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounterArea : MonoBehaviour
{
  [SerializeField] private List<GameObject> areaObjects;
  private GameObject player;
  private List<GameObject> stolenModifiers;
  private Transform playerModifierParent;

  private void Awake()
  {
    activateSpikes(false);
    areaObjects[areaObjects.Count - 1].SetActive(true);
    stolenModifiers = new List<GameObject>();
  }

  public void OnBossTrigger(GameObject _player, bool _active = true)
  {
    activateSpikes(_active);
    player = _player;
    if (_active)
    {
      stealModifiers();
    }
  }

  public void OnBossTrigger(bool _active = true)
  {
    activateSpikes(_active);
    if (!_active && stolenModifiers.Count > 0)
    {
      returnModifiers();
    }
  }

  private void activateSpikes(bool _active)
  {
    foreach (GameObject item in areaObjects)
    {
      item.SetActive(_active);
    }
  }

  private void stealModifiers()
  {
    EquipmentSlot[] equipmentSlots = FindObjectOfType<EquipmentUI>().slots;
    InventorySlot[] inventorySlots = FindObjectOfType<InventoryUI>().slots;
    Effect[] playerEffects = player.GetComponentsInChildren<Effect>();

    foreach (InventorySlot inventorySlot in inventorySlots)
    {
      inventorySlot.ClearSlot();
    }
    foreach (EquipmentSlot equipmentSlot in equipmentSlots)
    {
      equipmentSlot.ClearSlot();
    }

    foreach (Effect effect in playerEffects)
    {
      stolenModifiers.Add(effect.gameObject);
      playerModifierParent = effect.transform.parent;
      effect.transform.SetParent(transform);
    }
  }

  private void returnModifiers()
  {
    Inventory playerInventory = player.GetComponentInChildren<Inventory>();
    foreach (GameObject modifier in stolenModifiers)
    {
      playerInventory.AddItem(modifier);
      modifier.transform.SetParent(playerModifierParent);
      stolenModifiers.Remove(modifier);
    }
  }
}