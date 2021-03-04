﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeuchtpflanzeHealth : Health
{
  public int selfDestructTimeAfterHealthDeath;
  public ParticleSystem destroyParticles;

  public AudioClip deathSound;

  protected override void die()
  {
    base.die();
    foreach (Collider2D col2D in GetComponents<Collider2D>())
    {
      col2D.enabled = false;
    }
    // GetComponent<Collider2D>().enabled = false;
    GetComponent<MeshRenderer>().enabled = false;
    destroyParticles.Play();
    GetComponent<AudioSource>().PlayOneShot(deathSound);
    Destroy(this.gameObject, selfDestructTimeAfterHealthDeath);
  }
}
