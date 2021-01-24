using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnTriggerEnter : MonoBehaviour
{
    public float intensity = 3f;
    public float duration = 1f;

    public string tagName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            CinemachineShake.Instance.ShakeCamera(intensity, duration);
        }
    }
}
