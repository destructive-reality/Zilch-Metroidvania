using UnityEngine;

public class AgileEffect : Effect
{
    PlayerMovement playerMovement;
    private bool isDoublejumped;
    public override void ArmStart(bool value)
    {
        Debug.Log("Increase Player Attack Speed: " + value);
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        if (value)
            playerCombat.attackTime.addModifier(-0.1f);
        else
            playerCombat.attackTime.removeModifier(-0.1f);
    }
    public override void LegStart(bool value)
    {
        Debug.Log("Give Player Double Jump: " + value);
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        isDoublejumped = false;
    }
    public override void BodyStart(bool value)
    {
        Debug.Log("Increase Player Invincible Time: " + value);
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.startInvincibleTime.addModifier(1);
        else
            playerHealth.startInvincibleTime.removeModifier(1);
    }
    public override void HeadStart(bool value)
    {

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
