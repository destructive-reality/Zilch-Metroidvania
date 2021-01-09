﻿using UnityEngine;
using UnityEngine.Events;

public enum State { Idle, Jumping, Dashing, Attacking } // even necessarily when working with animator? MD

//Required Components
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MovementsBase
{
  public PlayerMasterController playerMasterController;

  [Header("Movement")]
  public Stat jumpForce; // Amount of force added when the player jumps.
  [Range(0, .3f)] [SerializeField] private float movementSmoothing = .16f; // How much to smooth out the movement
  [SerializeField] private LayerMask groundLayers; // A mask determining what is ground to the character
  [SerializeField] private Transform groundCheckTransform; // A position marking where to check if the player is grounded.
                                                           // public float runSpeed = 40f;                 // uses speed instead      MD
  private float horizontalAxisInput;

  #region Jumping
  private bool isGrounded; // Whether or not the player is grounded.
  [SerializeField] float checkRadius = .25f; // Radius of the overlap circle to determine if grounded
  private float jumpTimeCounter = 0f;
  public float jumpTime = 0.25f;
  public float gravityScaleUp = 4.2f;
  public float gravityScaleDown = 6.2f;
  private bool isJumping;
  public UnityEvent JumpTimeEnds;

  public ParticleSystem jumpParticles;

  #endregion

  #region WallSlide
  private bool isTouchingWall;
  public Transform wallCheck;
  private bool isWallSliding;
  public Stat wallSlidingSpeed;

  #endregion

  public Rigidbody2D playerRigidbody2D;
  // private bool movingRight = true;        // For determining which way the player is currently facing.
  private Vector2 currentVelocity = Vector2.zero;
  private SpriteRenderer spriteRenderer;
  public Animator playerAnimator;
  private State playerState;

  #region Dash
  public UnityEvent DashActions;
  public Stat dashSpeed;
  public float startDashTime = 0.1f; // The time it takes to dash
  private float dashTime; // remaining time of a dash
  public Stat startDashCooldown;
  private float dashCooldown; // remaining dash cooldown
  private float dashDirecton;
  #endregion
  public UnityEvent CollisionAction;

  [Header("Sounds")]
  public AudioSource playerAudioSource;
  public AudioClip jumpSound;
  [Range(0.0f, 1.0f)]
  public float jumpSoundVolume;
  public AudioClip dashSound;
  [Range(0.0f, 1.0f)]
  public float dashSoundVolume;
  public GroundSoundCollection defaultGroundSoundCollection;
  [SerializeField] private GroundSoundCollection currentGroundSoundCollection;
  [Range(0.0f, 1.0f)]
  public float walkSoundVolume;
  public float walkSoundRate;
  private float walkSoundTimer;

  private void Awake()
  {
    currentGroundSoundCollection = defaultGroundSoundCollection;
    dashTime = startDashTime;
    DashActions = new UnityEvent();
    DashActions.AddListener(hdlDash);
    CollisionAction = new UnityEvent();
    playerState = State.Idle;

    JumpTimeEnds = new UnityEvent();
    JumpTimeEnds.AddListener(hdlJumpEnd);

    walkSoundTimer = walkSoundRate;
  }

  private void FixedUpdate()
  {
    horizontalAxisInput = Input.GetAxisRaw("Horizontal");
    Vector2 targetVelocity = new Vector2(horizontalAxisInput * speed.getValue(), playerRigidbody2D.velocity.y);
    playerRigidbody2D.velocity = Vector2.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
  }

  private void Update()
  {
    //Update gravity scale of the player's rigidbody based on jumping or falling
    playerRigidbody2D.gravityScale = playerRigidbody2D.velocity.y > 0 ? gravityScaleUp : gravityScaleDown;

    bool wasGrounded = isGrounded;
    isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, checkRadius, groundLayers);

    if (!wasGrounded && isGrounded)
    {
      jumpParticles.Play();
    }

    playerAnimator.SetFloat("horizontalVelocity", Mathf.Abs(horizontalAxisInput)); //Play Animations correctly

    //Walking Sound
    if (Mathf.Abs(horizontalAxisInput) > 0 && isGrounded)
    {
      if (walkSoundTimer <= 0)
      {
        playerAudioSource.PlayOneShot(currentGroundSoundCollection.walkSoundAudioClips[Random.Range(0, currentGroundSoundCollection.walkSoundAudioClips.Count)], walkSoundVolume);
        walkSoundTimer = walkSoundRate;
      }
      walkSoundTimer -= Time.deltaTime;
    }
    else if (walkSoundTimer <= 0)
    {
      walkSoundTimer = walkSoundRate;
    }

    //Jumping
    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      isJumping = true;
      jumpTimeCounter = jumpTime;
      ApplyJumpForce();
      jumpParticles.Play();
      playerAudioSource.PlayOneShot(jumpSound, jumpSoundVolume);
    }

    if (Input.GetButton("Jump") && isJumping)
    {
      if (jumpTimeCounter > 0)
      {
        ApplyJumpForce();
        jumpTimeCounter -= Time.deltaTime;
      }
      else
      {
        JumpTimeEnds.Invoke();
      }
    }

    if (Input.GetButtonUp("Jump"))
    {
      isJumping = false;
    }

    //WallSliding
    isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayers);
    if (isTouchingWall && !isGrounded && horizontalAxisInput != 0)
      isWallSliding = true;
    else
      isWallSliding = false;

    if (isWallSliding)
    {
      playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, Mathf.Clamp(playerRigidbody2D.velocity.y, -wallSlidingSpeed.getValue(), float.MaxValue));
      // playerRigidbody2D.gravityScale = playerRigidbody2D.velocity.y > 0 ? gravityScaleUp - wallSlidingSpeed.getValue() : gravityScaleDown - wallSlidingSpeed.getValue();
    }

    //Dashing
    if (playerState == State.Idle || playerState == State.Jumping)
    {
      if (Input.GetButtonDown("Dash") && dashCooldown < Time.time)
      {
        playerAudioSource.PlayOneShot(dashSound, dashSoundVolume);
        playerState = State.Dashing;
        dashDirecton = isFacingRight ? 1 : -1;
        dashCooldown = Time.time + startDashCooldown.getValue();
        DashActions.Invoke();
      }
    }
    else if (playerState == State.Dashing)
    {
      if (dashTime <= 0)
      {
        dashDirecton = 0;
        if (isGrounded) // making new dash available when grounded, also possible with cooldown  MD
        {
          playerState = State.Idle;
          dashTime = startDashTime;
        }
      }
      else
      {
        dashTime -= Time.deltaTime;
        playerRigidbody2D.velocity = new Vector2(dashDirecton, 0) * dashSpeed.getValue();
      }
    }

    //Flip character sprite based on move direction
    //TODO Statt dem axisinput lieber die rigibody-velocity verwenden? BH
    if (horizontalAxisInput > 0 && !isFacingRight)
    {
      flip();
    }
    else if (horizontalAxisInput < 0 && isFacingRight)
    {
      flip();
    }
  }

  public void ApplyJumpForce()
  {
    playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce.getValue());
  }

  public void ApplyJumpForce(float _xValue)
  {
    playerRigidbody2D.velocity = new Vector2(_xValue, jumpForce.getValue());
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(groundCheckTransform.position, checkRadius);
  }

  public bool isAirborne()
  {
    return !isGrounded;
  }

  public void setGroundSoundCollection(GroundSoundCollection newCollection)
  {
    if (currentGroundSoundCollection != newCollection)
    {
      this.currentGroundSoundCollection = newCollection;
      Debug.Log("Ground Sound Collection switched to: " + newCollection.name);
    }
  }

  public bool getWallSliding()
  {
    return isWallSliding;
  }
  public State GetState()
  {
    return playerState;
  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    // Debug.Log("Collision detected on PlayerMovement");
    CollisionAction.Invoke();
  }
  private void hdlJumpEnd()
  {
    isJumping = false;
  }
  private void hdlDash()
  {
    playerAudioSource.PlayOneShot(dashSound, dashSoundVolume);
    playerState = State.Dashing;
    dashDirecton = isFacingRight ? 1 : -1;
    dashCooldown = Time.time + startDashCooldown.getValue();
  }
}