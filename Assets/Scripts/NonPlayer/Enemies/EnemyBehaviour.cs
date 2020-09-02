using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyState state;

    public void SetState(EnemyState _state)
    {
        state = _state;
        StartCoroutine(state.Start());
    }

    public void UseUpdate()
    {
        StartCoroutine(state.Update());
    }

    public abstract void ResetState();
}