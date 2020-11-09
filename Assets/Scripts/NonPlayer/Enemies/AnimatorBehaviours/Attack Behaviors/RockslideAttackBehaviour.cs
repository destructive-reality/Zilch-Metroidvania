using UnityEngine;

public class RockslideAttackBehaviour : RapidFireBehaviour
{
    private Transform tsfPlayer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        tsfPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (int i in fireFrames)
        {
            if (i == currentFrame)
            {
                FireProjectile(animator.transform, Vector2.down);
            }
        }
        currentFrame++;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    protected void FireProjectile(Transform _trfAnimator, Vector2 _dir)
    {
        // _dir.x = animatorScript.isFacingRight ? 1 : -1;

        Vector2 origin = new Vector2(tsfPlayer.position.x, combatScript.rangeAttackPoint.position.y);
        origin.y += projectileYOriginAdjustment;
        origin.x += Mathf.RoundToInt(Random.Range(-6, 6));

        Transform tsfProjectile = Instantiate(prefabProjectile, origin, Quaternion.identity);
        tsfProjectile.GetComponent<Projectile>().Setup(_dir, projectileLifeTime, projectileVelocity);
    }
}