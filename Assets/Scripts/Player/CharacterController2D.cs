using UnityEngine;
using UnityEngine.Events;

//Required Components
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask groundLayers;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheckTransform;                           // A position marking where to check if the player is grounded.
    public float runSpeed = 40f;
    private float horizontalAxisInput;

    #region Jumping
    private bool isGrounded;            // Whether or not the player is grounded.
    [SerializeField] float groundCheckRadius = .5f; // Radius of the overlap circle to determine if grounded

    private float jumpTimeCounter = 0f;
    public float jumpTime;
    private bool isJumping;

    #endregion

    private Rigidbody2D playerRigidbody2D;
    private bool isPlayerFacingRight = true;  // For determining which way the player is currently facing.
    private Vector2 currentVelocity = Vector2.zero;

    private SpriteRenderer spriteRenderer;
    public Animator playerAnimator;
    enum State { Idle, Jumping, Dashing, Attacking };     // even necessarily when working with animator? MD
    private State playerState;

    #region Dash

    public float dashSpeed = 70f;
    private float dashTime;         // remaining time of a dash
    public float startDashTime = 0.1f;     // The time it takes to dash
    private float dashDirecton;

    #endregion


    [Header("Sounds")]
    private AudioSource playerAudioSource;
    public AudioClip jumpSound;


    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        dashTime = startDashTime;
        playerState = State.Idle;
    }

    private void FixedUpdate()
    {
        horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        Vector2 targetVelocity = new Vector2(horizontalAxisInput * runSpeed, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = Vector2.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }
    
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayers);
        playerAnimator.SetFloat("horizontalVelocity", Mathf.Abs(horizontalAxisInput)); //Play Animations correctly

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            ApplyJumpForce();
            playerAudioSource.PlayOneShot(jumpSound);
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
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        //Dashing
        if (playerState == State.Idle || playerState == State.Jumping)
        {
            if (Input.GetButtonDown("Dash"))
            {
                playerState = State.Dashing;
                dashDirecton = isPlayerFacingRight ? 1 : -1;
            }
            // if (Input.GetButtonDown("Attack"))     // Attacking in this script? One script for all actions?  MD
            // {
            //     // playerState = State.Attacking;
            // }
        }
        else if (playerState == State.Dashing)
        {
            if (dashTime <= 0)
            {
                dashDirecton = 0;
                if (isGrounded)         // making new dash available when grounded, also possible with cooldown  MD
                {
                    playerState = State.Idle;
                    dashTime = startDashTime;
                }
            }
            else
            {
                dashTime -= Time.deltaTime;
                playerRigidbody2D.velocity = new Vector2(dashDirecton, 0) * dashSpeed;
            }
        }



        //Flip character sprite based on move direction
        //TODO Statt dem axisinput lieber die rigibody-velocity verwenden? BH
        if (horizontalAxisInput > 0 && !isPlayerFacingRight)
        {
            Flip();
        }
        else if (horizontalAxisInput < 0 && isPlayerFacingRight)
        {
            Flip();
        }


    }

    void ApplyJumpForce()
    {
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpForce);
    }

    //TODO hier vllt lieber den spriterenderer flippen? BH
    private void Flip()
    {
        isPlayerFacingRight = !isPlayerFacingRight;

        Vector3 localPlayerXScale = transform.localScale;
        localPlayerXScale.x *= -1;
        transform.localScale = localPlayerXScale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }

    public bool isAirborne()
    {
        return !isGrounded;
    }
}
