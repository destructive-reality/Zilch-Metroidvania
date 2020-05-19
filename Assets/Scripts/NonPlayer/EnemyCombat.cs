using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;

    public int attackPower = 30;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            // for disctance between Monster and player
            var playerHeading = player.position - this.transform.position;
            var playerDistance = playerHeading.magnitude;
            float playerAngle = Vector3.Angle(this.transform.forward, playerHeading);

            transform.Translate(playerHeading * 0.1f * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        collider.GetComponent<PlayerHealth>().getHit(attackPower, gameObject);
        Debug.Log("Hitting " + collider.tag + " for " + attackPower);
    }

}
