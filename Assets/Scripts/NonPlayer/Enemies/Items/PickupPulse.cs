using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPulse : MonoBehaviour
{
    public float scaleAmplitude = 0.2f;
    public float scaleFrequency = 3f;
    public float rotationsPerMinute = 40;

    public float opacityFrequency = 0.2f;

    private float startingScale;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        startingScale = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);

        spriteRenderer.color = new Color(1, 1, 1, .75f + (.25f * Mathf.Sin(opacityFrequency * Time.time)));

        float scale = startingScale + (scaleAmplitude * Mathf.Sin(scaleFrequency * Time.time));
        transform.localScale = new Vector3(scale, scale, 0);
    }
}
