using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounterArea : MonoBehaviour
{
  [SerializeField] private List<GameObject> areaObjects;

  private void Awake()
  {
    activateItems(false);
    areaObjects[areaObjects.Count - 1].SetActive(true);
  }

  public void OnBossTrigger(bool _active = true)
  {
    activateItems(_active);
  }

  private void activateItems(bool _active)
  {
    foreach (GameObject item in areaObjects)
    {
      item.SetActive(_active);
    }
  }
}