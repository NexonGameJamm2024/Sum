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
    private bool isMove;
    public static bool isGrounded;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        TryGetComponent(out rb);
        _MoveSpeed = MoveSpeed;
        _jumpForce = JumpForce;
        anim = GetComponent<Animator>();
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
        if(Input.GetAxis("Horizontal") > 0)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * _MoveSpeed, rb.velocity.y);
            GetComponent<SpriteRenderer>().flipX = false;
            if(gameObject.name == "JuSum")
            {
                anim.SetBool("isWalk", true);
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * _MoveSpeed, rb.velocity.y);
            GetComponent<SpriteRenderer>().flipX = true;
            if (gameObject.name == "JuSum")
            {
                anim.SetBool("isWalk", true);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            if (gameObject.name == "JuSum")
            {
                anim.SetBool("isWalk", false);
            }
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            if(gameObject.name != "WhyDownSum")
            {
                anim.SetTrigger("doJumping");
                anim.SetBool("isJump", true);
            }
            //Debug.Log("나 뛰었다");
        }
    }

    private void GroundCheck()
    {
        Debug.DrawRay(groundPoint.position, Vector3.down * 0.2f, new Color(0, 1, 0));
        var ang = Physics2D.RaycastAll(groundPoint.position, Vector2.down, 0.1f, GroundLayer);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && rb.velocity.y <0)
        {
            anim.SetBool("isJump", false);
        }
    }
}
