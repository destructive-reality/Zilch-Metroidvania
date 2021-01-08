using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AreaHider : MonoBehaviour
{
  [SerializeField] Light2D hidingLight;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      hidingLight.enabled = false;
    }
  }
}
