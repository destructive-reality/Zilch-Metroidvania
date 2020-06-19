using UnityEngine;

public class RangeEffect : Effect
{
    public override void ArmStart()
    {
        Debug.Log("Increase Player Attack Range");
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        playerCombat.attackRange += 1;
    }
    public override void LegStart()
    {
        Debug.Log("Increase Player Dash Time");
        PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.startDashTime += 0.2f;
    }
    public override void BodyStart()   
    {
        Debug.Log("Increase Player Invincible Time");
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        playerHealth.startInvincibleTime.addModifier(1);
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
