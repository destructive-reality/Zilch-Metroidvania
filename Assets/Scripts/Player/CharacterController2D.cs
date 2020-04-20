using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask groundLayers;                          // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheckTransform;                           // A position marking where to check if the player is grounded.

    [SerializeField] float groundedRadiusCheck = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Rigidbody2D playerRigidbody2D;
    private bool isPlayerFacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 currentVelocity = Vector3.zero;

    private SpriteRenderer spriteRenderer;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;



    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping");
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        bool wasGrounded = isGrounded;
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        //TODO Understand this.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedRadiusCheck, groundLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }


    public void Move(float horizontalMoveAxis, bool jump)
    {
        Vector3 targetVelocity = new Vector2(horizontalMoveAxis * 10f, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref currentVelocity, movementSmoothing);

        if (horizontalMoveAxis > 0 && !isPlayerFacingRight)
        {
            Flip();
        }
        else if (horizontalMoveAxis < 0 && isPlayerFacingRight)
        {
            Flip();
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }


    private void Flip()
    {
        isPlayerFacingRight = !isPlayerFacingRight;

        Vector3 localPlayerXScale = transform.localScale;
        localPlayerXScale.x *= -1;
        transform.localScale = localPlayerXScale;
    }
}
