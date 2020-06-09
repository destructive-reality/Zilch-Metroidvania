using UnityEngine;

public class PowerEffect : Effect
{

    public override void ArmStart()
    {

    }
    public override void LegStart()
    {

    }
    public override void BodyStart()
    {
        Debug.Log("Increase Player Health");
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        playerHealth.maxHealth += 10;
        playerHealth.setHealth(playerHealth.maxHealth);
        // player.GetComponent<PlayerHealth>().maxHealth += 10;
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
