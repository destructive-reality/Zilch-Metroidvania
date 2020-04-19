using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public vars
    public float horizontalSpeed;
    public float jumpForce;

    //Player components
    private Rigidbody2D playerRigidbody;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
