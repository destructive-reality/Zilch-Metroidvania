using UnityEngine;

public abstract class MeleeHorizontalMovingEnemy : HorizontalMovingEnemy
{
    protected MeleeEnemyCombat combatScript;
    protected Animator animator;
    protected Vector2 playerPosition;

    protected virtual void Start()
    {
        combatScript = gameObject.GetComponent<MeleeEnemyCombat>();
        animator = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public float DistanceToPlayer(Vector2 _fromPoint)
    {
        float range = Vector2.Distance(_fromPoint, playerPosition);
        return range;
    }
}