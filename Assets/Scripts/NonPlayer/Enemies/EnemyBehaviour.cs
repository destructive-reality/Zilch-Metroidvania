using UnityEngine;

public abstract class EnemyBehaviour : MovementsBase
{
  protected EnemyState state;

  public void SetState(EnemyState _state)
  {
    state = _state;
    StartCoroutine(state.Start());
  }

  public abstract void ResetState();
}