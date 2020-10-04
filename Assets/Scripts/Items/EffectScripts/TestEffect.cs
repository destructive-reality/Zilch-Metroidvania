using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : Effect
{
    PlayerMovement playerMovement;
    private bool isDoublejumped;
    private int attackPowerBoost = 1; 
    private float dashTimeBoost = 0.2f;
    private int healthBoost = 1;

    public override void ArmStart(bool value)
    {
        Debug.Log("Increase Player Damage: " + value);
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        if (value)
            playerCombat.attackPower.addModifier(attackPowerBoost);
        else
            playerCombat.attackPower.removeModifier(attackPowerBoost);
    }
    public override void LegStart(bool value)
    {
        Debug.Log("Give Player Double Jump: " + value);
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        isDoublejumped = false;
    }
    public override void HeadStart(bool value = true)
    {
        Debug.Log("Increase Player Dash Time: " + value);
        PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        if (value)
            playerMovement.startDashTime += dashTimeBoost;
        else
            playerMovement.startDashTime -= dashTimeBoost;
    }
    public override void WeaponStart(bool value)
    {
        Debug.Log("Increase Player Health: " + value);
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.maxHealth.addModifier(healthBoost);
        else
            playerHealth.maxHealth.removeModifier(healthBoost);
        playerHealth.setHealth((int)playerHealth.maxHealth.getValue());
    }
    public override void ArmUpdate()
    {

    }
    public override void LegUpdate()
    {
        if (playerMovement.isAirborne() && Input.GetButtonDown("Jump") && !isDoublejumped)
        {
            Debug.Log("Execute Double Jump");
            playerMovement.ApplyJumpForce();
            isDoublejumped = true;
        }
        if (isDoublejumped && !playerMovement.isAirborne())
        {
            isDoublejumped = !isDoublejumped;
        }
    }
    public override void WeaponUpdate()
    {

    }
    public override void HeadUpdate()
    {

    }
}
