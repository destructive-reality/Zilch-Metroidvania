using UnityEngine;

public class AgileEffect : Effect
{
    private PlayerMovement playerMovement;
    private bool isDoublejumped;
    private float attackSpeedBoost = -0.1f;
    private int iFrameTimeBoost = 1;
    public override void ArmStart(bool value)
    {
        Debug.Log("Increase Player Attack Speed: " + value);

        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        if (value)
            playerCombat.attackTime.addModifier(attackSpeedBoost);
        else
            playerCombat.attackTime.removeModifier(attackSpeedBoost);
    }
    public override void LegStart(bool value)
    {
        Debug.Log("Give Player Double Jump: " + value);
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        isDoublejumped = false;
    }
    public override void WeaponStart(bool value)
    {
        Debug.Log("Increase Player Invincible Time: " + value);

        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.startInvincibleTime.addModifier(iFrameTimeBoost);
        else
            playerHealth.startInvincibleTime.removeModifier(iFrameTimeBoost);
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
    public override void WeaponUpdate()
    {

    }
    public override void HeadUpdate()
    {

    }
}
