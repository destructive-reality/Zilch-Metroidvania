using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StepSoundCollectionApplier : MonoBehaviour
{
    public GroundSoundCollection groundSoundCollection;
    // Start is called before the first frame update

   private void OnCollisionEnter2D(Collision2D other) {
       if (this.groundSoundCollection && other.gameObject.CompareTag("Player")) {
           other.gameObject.GetComponent<PlayerMovement>().setGroundSoundCollection(this.groundSoundCollection);
       }
   }
}
