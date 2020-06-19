using UnityEngine;

public class PowerEffect : Effect
{
    public override void ArmStart()
    {
        Debug.Log("Increase Player Damage");
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        playerCombat.attackPower += 10;
    }
    public override void LegStart()
    {
        Debug.Log("Increase Player DashSpeed");
        PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.dashSpeed.addModifier(15);
    }
    public override void BodyStart()
    {
        Debug.Log("Increase Player Health");
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        playerHealth.maxHealth += 10;
        playerHealth.setHealth(playerHealth.maxHealth);
    }
    public override void HeadStart()
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
