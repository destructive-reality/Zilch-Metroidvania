using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;

    const float smoothTime = 0.04F;
    private Vector3 velocity = Vector3.zero;
    Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.localPosition;
    }

    public void Move(float deltaMove)
    {
        targetPosition.x -= deltaMove * parallaxFactor;
    }

    private void Update()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, smoothTime);
    }
}
