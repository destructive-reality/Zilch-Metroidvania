using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public vars
    public float horizontalSpeed;
    public float jumpForce;
    public float dashForce;
    public bool isGrounded = false;

    //Player components
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        //Flip Sprite depending on orientation
        playerSpriteRenderer.flipX = horizontalAxis < 0;


        //Movement Left/Right
        playerRigidbody.MovePosition(playerRigidbody.position + new Vector2(horizontalAxis * horizontalSpeed * Time.deltaTime, 0));

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerRigidbody.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerRigidbody.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
