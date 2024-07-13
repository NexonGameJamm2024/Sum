using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    [SerializeField] private Transform groundPoint = null;
    public LayerMask GroundLayer;

    private float _MoveSpeed;
    private float _jumpForce;

    public static bool isDrag;
    public static bool isGrounded;
    private Rigidbody2D rb;

    private void Start()
    {
        TryGetComponent(out rb);
        _MoveSpeed = MoveSpeed;
        _jumpForce = JumpForce;
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

        if (!isGrounded)
        {
            Debug.Log("아아아아아아아아아아아");
        }

    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * _MoveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Debug.Log("나 뛰었다");
        }
    }

    private void GroundCheck()
    {
        Debug.DrawRay(groundPoint.position, Vector3.down * 0.2f, new Color(0, 1, 0));
        var ang = Physics2D.RaycastAll(groundPoint.position, Vector2.down, 0.2f, GroundLayer);
        isGrounded = ang.Length > 0;
    }

    public void ResetValues(Vector3 ResetPosition)
    {
        isDrag = false;
        isGrounded = false;
        _MoveSpeed = MoveSpeed;
        _jumpForce = JumpForce;

        transform.position = ResetPosition;
    }

}
