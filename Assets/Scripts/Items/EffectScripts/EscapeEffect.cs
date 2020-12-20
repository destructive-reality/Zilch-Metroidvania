using UnityEngine;

public class EscapeEffect : Effect {
    private PlayerMovement playerMovement;
    public override void ArmStart (bool value) {

    }
    public override void LegStart (bool value) {
        Debug.Log ("Give Player WallJump Jump: " + value);
        playerMovement = gameObject.GetComponentInParent<PlayerMovement> ();
    }
    public override void WeaponStart (bool value) {

    }
    public override void HeadStart (bool value) {

    }
    public override void ArmUpdate () {

    }
    public override void LegUpdate () {
        if (playerMovement.isAirborne () && Input.GetButtonDown ("Jump")) {
            Debug.Log ("Execute Wall Jump");
            if (true) {
                playerMovement.ApplyJumpForce ();

            }
        }
    }
    public override void WeaponUpdate () {

    }
    public override void HeadUpdate () {

    }
}