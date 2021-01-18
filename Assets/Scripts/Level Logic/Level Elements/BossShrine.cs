using UnityEngine;

public class BossShrine : Shrine
{
  [SerializeField] private int minModifierCount;
  [SerializeField] private Transform prefabBoss;
  private bool isReady = false;
  private bool isActive = false;

  protected override void OnTriggerEnter2D(Collider2D other)
  {
    base.OnTriggerEnter2D(other);

    if (isActive)
    {
      isInteractable = false;
    }
    if (other.CompareTag("Player"))
    {
      if (checkPlayerModifiers(other.gameObject))
      {
        isReady = true;
      }
      Debug.Log("Player at BossShrine");
    }
  }

  protected override void OnTriggerExit2D(Collider2D other)
  {
    base.OnTriggerExit2D(other);

    if (isReady)
    {
      Transform tsfBoss = Instantiate(prefabBoss, transform.position, Quaternion.identity);
      // GameObject.Destroy(this);
      // Start Boss-Transformation  MD
      // isActive = true;
    }
  }

  private bool checkPlayerModifiers(GameObject _player)
  {
    int modifierCount = _player.GetComponentInChildren<Inventory>().gameObject.transform.childCount;
    Debug.Log(modifierCount);
    if (modifierCount >= minModifierCount)
    {
      return true;
    }
    return false;
  }
}