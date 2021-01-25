using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounterArea : MonoBehaviour
{
  [SerializeField] private List<GameObject> areaObjects;

  private void Start()
  {
    activateItems(false);
  }

  public void OnBossTrigger()
  {
    activateItems(true);
  }

  private void activateItems(bool _active)
  {
    foreach (GameObject item in areaObjects)
    {
      item.SetActive(_active);
    }
  }
}