using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public vars
    public float horizontalSpeed;
    public float jumpForce;
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

        transform.Translate(new Vector2(horizontalAxis * horizontalSpeed * Time.deltaTime, 0));
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
