using UnityEngine;

public class GroundForce : EnemyCombat
{
  private static readonly Vector3 SpritePositionOffset = new Vector3(0, -2.5f);
  private static readonly Vector3 ScalingVector = new Vector3(1, 0.2f, 1);
  [SerializeField] private Collider2D[] damageCollider;
  private Vector3 startScale;
  private Vector3 startPosition;
  private int scaler;
  private float liveTime;

  public void Setup(float _timeToExist, int _damage = 1)
  {
    attackPower.addModifier(_damage);
    liveTime = _timeToExist;
    Destroy(gameObject, _timeToExist);

    // Invoke("changeScaling", 0.3f);
    // Invoke("changeScaling", 0.6f);
    // Invoke("changeScaling", liveTime / 2);
    // Invoke("changeScaling", liveTime / 5 * 4);
  }

  //   private void Start()
  //   {
  //     scaler = 0;

  //     Vector3 scale = transform.localScale;
  //     startScale = transform.localScale;
  //     startPosition = transform.position;
  //     transform.position += GroundForce.SpritePositionOffset;
  //     scale.Scale(GroundForce.ScalingVector);
  //     transform.localScale = scale;

  //     enableColliders(false);
  //   }

  //   private void Update()
  //   {
  //     scaleObject(scaler);
  //   }

  private void scaleObject(int _factor = 1)
  {
    Vector3 translation = Vector3.up * _factor;
    if (transform.position.y < startPosition.y * _factor)
    {
      if (startPosition.y > 0)
        translation.Scale(startPosition - GroundForce.SpritePositionOffset);
      else
        translation.Scale(-startPosition - GroundForce.SpritePositionOffset);

      transform.Translate(translation * Time.deltaTime);
    }

    if (transform.localScale.y * _factor < startScale.y)
    {
      translation = Vector3.up * _factor;
      translation.Scale(GroundForce.ScalingVector * Time.deltaTime * 20);
      if (transform.localScale.y + translation.y > 0.3f)
        transform.localScale += translation;
    }
  }

  private void changeScaling()
  {
    if (scaler > -1)
    {
      scaler--;
      enableColliders();
    }
    else
    {
      scaler = 1;
      enableColliders(false);
    }
  }

  private void enableColliders(bool _isEnabled = true)
  {
    foreach (Collider2D collider in damageCollider)
    {
      collider.enabled = _isEnabled;
    }
  }
}