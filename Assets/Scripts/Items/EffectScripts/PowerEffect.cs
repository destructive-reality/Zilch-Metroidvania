using UnityEngine;

public class PowerEffect : Effect
{
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
        Debug.Log("Increase Player DashSpeed: " + value);
        PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        if (value)
            playerMovement.dashSpeed.addModifier(15);
        else
            playerMovement.dashSpeed.removeModifier(15);
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
    public override void HeadStart(bool value)
    {

    }
    public override void ArmUpdate()
    {

    }
    public override void LegUpdate()
    {

    }
    public override void BodyUpdate()
    {

    }
    public override void HeadUpdate()
    {

    }
}
