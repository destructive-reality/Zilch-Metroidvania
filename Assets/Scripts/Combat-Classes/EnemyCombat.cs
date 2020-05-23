﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatBase
{

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        collider.GetComponent<PlayerHealth>().getHit(attackPower, gameObject);
    }
}
