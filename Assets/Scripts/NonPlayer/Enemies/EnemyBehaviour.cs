using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public bool isFacingRight = true;
    public Stat speed;
    protected EnemyState state;

    public void SetState(EnemyState _state)
    {
        state = _state;
        StartCoroutine(state.Start());
    }

    public abstract void ResetState();

    public void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localXScale = transform.localScale;
        localXScale.x *= -1;
        transform.localScale = localXScale;
    }
}