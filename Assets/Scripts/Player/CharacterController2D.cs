using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
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


    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("horizontalVelocity", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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

        //TODO don't call this every frame
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
}
