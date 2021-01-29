using UnityEngine;

public class Health : MonoBehaviour
{
  public Stat maxHealth;
  protected int currentHealth;
  [SerializeField] protected Animator animator;

  protected virtual void Start()
  {
    setHealth((int)maxHealth.getValue());
  }

  public virtual void setHealth(int _healthToSetTo)
  {
    if (_healthToSetTo < 0)
    {
      Debug.LogError("New health cannot be below zero.");
    }
    if (_healthToSetTo > maxHealth.getValue())
    {
      Debug.LogError("New health cannot be above max health.");
    }
    currentHealth = _healthToSetTo;
  }

  public virtual void applyDamage(int _damageToTake)
  {
    // Debug.Log(gameObject.name + " gets Hit for " + _damageToTake);
    setHealth(Mathf.Clamp(currentHealth - _damageToTake, 0, (int)maxHealth.getValue()));

    if (isDead())
    {
      die();
    }
  }

  public void heal(int _amountToHeal)
  {
    setHealth(Mathf.Clamp(currentHealth + _amountToHeal, 0, (int)maxHealth.getValue()));
  }

  public void healToMaxHealth()
  {
    heal((int)maxHealth.getValue());
  }

  protected virtual void die()
  {
    // Debug.Log(gameObject.name + " is dying. Bye!");
  }

  public bool isDead()
  {
    return currentHealth <= 0;
  }

  public int getCurrentHealth()
  {
    return currentHealth;
  }
}
