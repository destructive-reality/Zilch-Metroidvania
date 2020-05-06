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

    [SerializeField] float groundCheckRadius = .5f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Rigidbody2D playerRigidbody2D;
    private bool isPlayerFacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 currentVelocity = Vector3.zero;

    private SpriteRenderer spriteRenderer;
    public Animator playerAnimator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    enum State {Idle, Jumping, Dashing, Attacking};     // even necessarily when working with animator? MD
    private State playerState;
    public float dashSpeed = 70f;
    private float dashTime;         // remaining time of a dash
    public float startDashTime = 0.1f;     // The time it takes to dash
    private float dashDirecton;

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


    private void Update()
    {
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
                // playerRigidbody2D.velocity = Vector2(playerRigidbody2D.velocity.x * 0.5f, playerRigidbody2D.velocity.y);
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
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("horizontalVelocity", Mathf.Abs(horizontalMove)); //Play Animations correctly

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            playerAudioSource.PlayOneShot(jumpSound);
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(horizontalMove, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        if (horizontalMove > 0 && !isPlayerFacingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && isPlayerFacingRight)
        {
            Flip();
        }

        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundCheckRadius, groundLayers);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }


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
