using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : KnockbackHealth
{
    public PlayerMasterController playerMasterController;
    public float startInvincibleTime = 1.5f;
    private float invincibleTime = 0f;

    protected override void Start()
    {
        setHealth(PlayerPrefs.GetInt("PlayerHealth", maxHealth));
        Debug.Log("end of playerhealth start");
    }

    public override void setHealth(int healthToSetTo)
    {
        base.setHealth(healthToSetTo);
        UIController.Instance.setHealthInUI(currentHealth, maxHealth);
    }

    public new void getHit(int damage, GameObject damagingObject)
    {
        if (Time.time >= invincibleTime)
        {
            Debug.Log("Time: " + Time.time + "; invincibleTime: " + invincibleTime);
            base.getHit(damage, damagingObject);
            invincibleTime = Time.time + startInvincibleTime;
        }
    }
}
