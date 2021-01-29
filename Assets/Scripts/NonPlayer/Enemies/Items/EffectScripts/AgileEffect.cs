using UnityEngine;

public class AgileEffect : Effect
{
    [SerializeField] private float attackSpeedBoost = -0.1f;
    private PlayerMovement playerMovement;
    private PlayerCombat2D playerCombat2D;
    private bool isDoublejumped;
    public int knockbackForceReduction;
    private Transform attackPoint;
    private Vector2 attackPointStartPosition;

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
        Debug.Log("Decrease Player Knockback: " + value);
        PlayerHealth playerHealth = gameObject.GetComponentInParent<PlayerHealth>();
        if (value)
            playerHealth.knockbackForce.addModifier(-knockbackForceReduction);
        else
            playerHealth.knockbackForce.removeModifier(-knockbackForceReduction);
    }
    public override void HeadStart(bool value)
    {
        playerCombat2D = gameObject.GetComponentInParent<PlayerCombat2D>();
        Debug.Log("Let Player move AttackPoint: " + value);
        PlayerCombat2D playerCombat = gameObject.GetComponentInParent<PlayerCombat2D>();
        attackPoint = playerCombat.attackPoint;
        attackPointStartPosition = new Vector2(attackPoint.position.x, attackPoint.position.y);
    }
    public override void ArmUpdate()
    {

    }
    public override void LegUpdate()
    {
        if (isDoublejumped && !playerMovement.isAirborne())
        {
            isDoublejumped = !isDoublejumped;
        }
        if (playerMovement.isAirborne() && Input.GetButtonDown("Jump") && !isDoublejumped)
        {
            if (Object.FindObjectOfType<EscapeEffect>().IsWallJumpable)
            {
                Debug.Log("Use Walljump instead of Double Jump");
                return;
            }
            Debug.Log("Execute Double Jump");
            playerMovement.ApplyJumpForce();
            isDoublejumped = true;
        }
    }
    public override void WeaponUpdate()
    {

    }
    public override void HeadUpdate()
    {
        Debug.Log(Input.GetAxis("Vertical"));

        if (Input.GetAxis("Vertical") < 0 && playerCombat2D.attackDirection == ATTACK_DIRECTION.START)
        {
            playerCombat2D.attackDirection = ATTACK_DIRECTION.UP;
            attackPoint.Translate(-1, 1.5f, 0, Space.Self);
        }
        if (Input.GetAxis("Vertical") > 0 && playerCombat2D.attackDirection == ATTACK_DIRECTION.START)
        {
            playerCombat2D.attackDirection = ATTACK_DIRECTION.DOWN;
            attackPoint.Translate(-1, -1.5f, 0, Space.Self);
        }
        if (Input.GetAxis("Vertical") == 0 && playerCombat2D.attackDirection == ATTACK_DIRECTION.UP)
        {
            playerCombat2D.attackDirection = ATTACK_DIRECTION.START;
            attackPoint.Translate(1, -1.5f, 0, Space.Self);
        }
        if (Input.GetAxis("Vertical") == 0 && playerCombat2D.attackDirection == ATTACK_DIRECTION.DOWN)
        {
            playerCombat2D.attackDirection = ATTACK_DIRECTION.START;
            attackPoint.Translate(1, 1.5f, 0, Space.Self);
        }
    }
}