using UnityEngine;

public class BossShrine : Shrine
{
  [SerializeField] private int minModifierCount;
  [SerializeField] private Transform prefabBoss;
  [SerializeField] private BossEncounterArea encounterArea;
  private bool isReady = false;

  protected override void OnTriggerEnter2D(Collider2D other)
  {
    base.OnTriggerEnter2D(other);

    if (other.CompareTag("Player"))
    {
      if (checkPlayerModifiers(other.gameObject))
      {
        isReady = true;
        encounterArea.OnBossTrigger(other.gameObject);
      }
      Debug.Log("Player at BossShrine");
    }
  }

  protected override void OnTriggerExit2D(Collider2D other)
  {
    base.OnTriggerExit2D(other);

    if (isReady)
    {
      Vector2 bossSpawnPosition = new Vector2(transform.position.x, transform.position.y - 1);
      Transform tsfBoss = Instantiate(prefabBoss, bossSpawnPosition, Quaternion.identity);
      GameObject.Destroy(gameObject);
      // Start Boss-Transformation  MD
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