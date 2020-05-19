using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public Animator animator;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        collider.GetComponent<PlayerHealth>().getHit(damage, gameObject);
    }

}
