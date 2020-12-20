using UnityEngine;

public class EscapeEffect : Effect {
    private PlayerMovement playerMovement;
    [SerializeField] float xJumpForce;
    [SerializeField] float yJumpForce;
    public bool IsWallJumpable {
        get;
        private set;
    }
    public override void ArmStart (bool value) {

    }
    public override void LegStart (bool value) {
        Debug.Log ("Give Player WallJump Jump: " + value);
        playerMovement = gameObject.GetComponentInParent<PlayerMovement> ();
        IsWallJumpable = false;
    }
    public override void WeaponStart (bool value) {

    }
    public override void HeadStart (bool value) {

    }
    public override void ArmUpdate () {

    }
    public override void LegUpdate () {
        if (playerMovement.isAirborne () && playerMovement.getWallSliding ()) {
            IsWallJumpable = true;
            if (Input.GetButtonDown ("Jump")) {
                Debug.Log ("Execute Wall Jump");
                int direction = 0;
                if (playerMovement.isFacingRight) {
                    direction = -1;
                } else {
                    direction = 1;
                }
                playerMovement.jumpForce.addModifier (yJumpForce);
                playerMovement.ApplyJumpForce (direction * xJumpForce);
                playerMovement.jumpForce.removeModifier (yJumpForce);
            }
        } else
            IsWallJumpable = false;
    }
    public override void WeaponUpdate () {

    }
    public override void HeadUpdate () {

    }
}