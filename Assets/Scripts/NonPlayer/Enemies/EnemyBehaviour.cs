using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected EnemyState state;
    // protected Coroutine currentCoroutine;

    public void SetState(EnemyState _state)
    {
        state = _state;
        // currentCoroutine = 
        StartCoroutine(state.Start());
    }

    public abstract void ResetState();
}