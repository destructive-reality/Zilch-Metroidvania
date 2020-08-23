using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEffect : Effect
{
    PlayerMovement playerMovement;
    private bool isDoublejumped;

    public override void ArmStart(bool value)
    {
        Debug.Log("Increase Player Damage: " + value);
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        if (value)
            playerCombat.attackPower.addModifier(10);
        else
            playerCombat.attackPower.removeModifier(10);
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
            playerMovement.startDashTime += 0.2f;
        else
            playerMovement.startDashTime -= 0.2f;
    }
    public override void BodyStart(bool value)
    {
        Debug.Log("Increase Player Health: " + value);
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.maxHealth.addModifier(10);
        else
            playerHealth.maxHealth.removeModifier(10);
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
    public override void BodyUpdate()
    {

    }
    public override void HeadUpdate()
    {

    }
}
