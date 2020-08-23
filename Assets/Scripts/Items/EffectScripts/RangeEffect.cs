using UnityEngine;

public class RangeEffect : Effect
{
    public override void ArmStart(bool value = true)
    {
        Debug.Log("Increase Player Attack Range: " + value);
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        if (value)
            playerCombat.attackRange.addModifier(0.3f);
        else 
            playerCombat.attackRange.removeModifier(0.3f);
    }
    public override void LegStart(bool value = true)
    {
        Debug.Log("Increase Player Dash Time: " + value);
        PlayerMovement playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        if (value)
            playerMovement.startDashTime += 0.2f;
        else
            playerMovement.startDashTime -= 0.2f;
    }
    public override void BodyStart(bool value = true)
    {
        Debug.Log("Increase Player Invincible Time: " + value);
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.startInvincibleTime.addModifier(1);
        else
            playerHealth.startInvincibleTime.removeModifier(1);
    }
    public override void HeadStart(bool value = true)
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
