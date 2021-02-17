using System.Collections.Generic;
using UnityEngine;

public class DoofEnemyAnimator : MeleeHorizontalMovingEnemy
{
    [SerializeField] private float aggressionRange = 7f;
    private Vector2 position;
    private Vector2 startPosition;

    [Header("Sounds")]
    public List<AudioClip> walkingAudioClips;
    
    [Range(0f, 4f)]
    public float walkingAudioVolume = 1;
    private AudioSource enemyAudioSource;

    protected override void Start()
    {
        base.Start();
        position = gameObject.transform.position;
        startPosition = new Vector2(position.x, position.y);
    }

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        position = gameObject.transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        AnimatorStateInfo currentAnimation = animator.GetCurrentAnimatorStateInfo(0);

        if (!currentAnimation.IsName("WalkTowards") && DistanceToPlayer(position) <= aggressionRange && !animator.GetBool("isPlayerNear"))
        {
            Debug.Log("DoofEnemy is near Player");
            animator.SetBool("isPlayerNear", true);
        }
        else if (currentAnimation.IsName("WalkTowards"))
        {
            if (playerPosition.x < position.x && isFacingRight)
            {
                flip();
            }
            else if (playerPosition.x > position.x && !isFacingRight)
            {
                flip();
            }
            if (DistanceToPlayer(position) > aggressionRange && !animator.GetBool("isNeedingReset"))
            {
                Debug.Log("Player out of Doof's aggro range");      // Dieser Teil wird komischer weise ausgeführt, 
                animator.SetBool("isNeedingReset", true);           // nachdem das OnStateEnter von Reset-State ausgeführt wurde (MD)
            }
            else if (animator.GetBool("isNeedingReset") && DistanceToPlayer(position) < aggressionRange)
            {
                animator.SetBool("isNeedingReset", false);
            }
            if (DistanceToPlayer(combatScript.attackPoint.position) < combatScript.attackRange.getValue())
            {
                // Debug.Log("Doof in attackRange");
                animator.SetTrigger("inAttackRange");
            }
        }
        else if (currentAnimation.IsName("Attack") && DistanceToPlayer(position) > aggressionRange)
        {
            Debug.Log("Player out of Doof's aggro range");
            animator.SetBool("isNeedingReset", true);
        }
        else if (currentAnimation.IsName("Reset") && Vector2.Distance(startPosition, position) < 2f)
        {
            Debug.Log("Doof back to Idle");
            animator.SetTrigger("reseted");
        }
    }

    public override void ResetState()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 StartPosition
    {
        get { return startPosition; }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, aggressionRange);
    }

    public void triggerRandomWalkSoundClip()
    {
        enemyAudioSource.PlayOneShot(walkingAudioClips[Random.Range(0, walkingAudioClips.Count)], walkingAudioVolume);
    }
}
