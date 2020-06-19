using UnityEngine;

public class AgileEffect : Effect
{
    PlayerMovement playerMovement;
    private bool isDoublejumped;
    public override void ArmStart()
    {
        Debug.Log("Increase Player Attack Speed");
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        playerCombat.attackTime -= 0.1f;
    }
    public override void LegStart()
    {
        Debug.Log("Give Player Double Jump");
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        isDoublejumped = false;
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
        Debug.Log("Check for double Jump");
        if (playerMovement.isAirborne() && Input.GetButtonDown("Jump") && !isDoublejumped)
        {
            Debug.Log("Execute Double Jump");
            playerMovement.ApplyJumpForce();
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
