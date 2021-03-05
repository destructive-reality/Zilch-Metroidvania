using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounterArea : MonoBehaviour
{
  [SerializeField] private List<GameObject> areaObjects;
  [SerializeField] private Transform bossShrinePrefab;
  private GameObject player;
  private List<GameObject> stolenModifiers;
  private Transform playerModifierParent;
  private Transform tsfBoss;
  private Vector3 bossShrinePosition;

  private void Awake()
  {
    activateSpikes(false);
    areaObjects[areaObjects.Count - 1].SetActive(true);
    stolenModifiers = new List<GameObject>();
  }

  public void OnBossTrigger(GameObject _player, Vector3 _shrinePosition, bool _active = true)
  {
    activateSpikes(_active);
    bossShrinePosition = _shrinePosition;

    player = _player;
    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
    playerHealth.onDeath.RemoveListener(resetBossArea);
    playerHealth.onDeath.AddListener(resetBossArea);
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

  public void spawnBoss(Transform _bossPrefab, Vector3 _bossSpawnPosition)
  {
    tsfBoss = Instantiate(_bossPrefab, _bossSpawnPosition, Quaternion.identity);
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
    for (int i = stolenModifiers.Count - 1; i > 0; i--)
    {
      GameObject modifier = stolenModifiers[i];
      playerInventory.AddItem(modifier);
      modifier.transform.SetParent(playerModifierParent);
      stolenModifiers.Remove(modifier);
    }
  }

  private void resetBossArea()
  {
    Debug.Log("ResetBoss");
    if (stolenModifiers.Count > 0)
    {
      Transform tsfBoss = Instantiate(bossShrinePrefab, bossShrinePosition, Quaternion.identity);
      activateSpikes(false);
      areaObjects[areaObjects.Count - 1].SetActive(true);
      returnModifiers();
    }
    if (tsfBoss)
    {
      Destroy(tsfBoss.gameObject);
    }
  }
}