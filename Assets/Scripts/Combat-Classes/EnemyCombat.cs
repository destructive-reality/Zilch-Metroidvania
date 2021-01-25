using UnityEngine;

public class EnemyCombat : Damaging
{
  protected void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.CompareTag("Player"))
    {
      collider.GetComponent<PlayerHealth>().getHit((int)attackPower.getValue(), transform.position);
    }
  }
}