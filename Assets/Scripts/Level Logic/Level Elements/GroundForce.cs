using UnityEngine;

public class GroundForce : EnemyCombat
{
    Vector3 scale;
    private void Start()
    {
        scale = transform.localScale;
    }

    public void Setup(float _timeToExist, int _damage = 0)
    {
        attackPower.addModifier(_damage); 
        Destroy(gameObject, _timeToExist);
    }
}