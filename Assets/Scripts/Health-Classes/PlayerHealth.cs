﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : KnockbackHealth
{
    public PlayerMasterController playerMasterController;
    public Stat startInvincibleTime;
    private float invincibleTime = 0f;

    protected override void Start()
    {
        setHealth(PlayerPrefs.GetInt("PlayerHealth", (int) maxHealth.getValue()));
    }

    public override void setHealth(int healthToSetTo)
    {
        base.setHealth(healthToSetTo);
        UIController.Instance.setHealthInUI(currentHealth, (int) maxHealth.getValue());
    }

    public new void getHit(int damage, GameObject damagingObject)
    {
        if (Time.time >= invincibleTime)
        {
            Debug.Log("Time: " + Time.time + "; invincibleTime: " + invincibleTime);
            base.getHit(damage, damagingObject);
            invincibleTime = Time.time + startInvincibleTime.getValue();
            PostProController.Instance.TriggerChromaticAberrationDamageAnimation();
        }
    }
}
