using UnityEngine;

public class HealthReductionTrickster : MonoBehaviour
{
  [SerializeField] private int healthReduction;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

      int newPlayerHealthAmount = playerHealth.getCurrentHealth() - healthReduction;
      newPlayerHealthAmount = (int)Mathf.Clamp(newPlayerHealthAmount, 1, playerHealth.maxHealth.getValue());
      playerHealth.setHealth(newPlayerHealthAmount);

      Destroy(this.gameObject);
    }
  }
}