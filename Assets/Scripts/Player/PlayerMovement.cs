using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public LayerMask GroundLayer;

    public static bool isDrag;
    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        if (!isDrag)
        {
            Move();
        }
    }

    private void Update()
    {
        if (!isDrag)
        {
            GroundCheck();
            Jump();
        }

    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * MoveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (!isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = true;
            Debug.Log("나 뛰었다");
        }
    }

    private void GroundCheck()
    {
        if (rb.velocity.y < 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, GroundLayer);
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                {
                    isGrounded = false;
                }
            }
        }
    }


}
