using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpingForce = 3f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.CompareTag("Player") && otherGameObject.GetComponent<PlayerMovement>().isAirborne())
        {
            //TODO ist das schön so? vllt lieber auf dem Player?
            otherGameObject
                .GetComponent<PlayerMovement>()
                .playerRigidbody2D
                .AddForce(new Vector2(0f, jumpingForce), ForceMode2D.Impulse);
        }
    }
}
